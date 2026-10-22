using FluentValidation;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.Extensions.Localization;
using Sansheera.WholesalePlatform.Api.Contracts.Common;
using Sansheera.WholesalePlatform.Api.Resources;
using Sansheera.WholesalePlatform.Application.Abstractions.Persistence;
using Sansheera.WholesalePlatform.Application.Common.Exceptions;
using System.Security.Claims;

namespace Sansheera.WholesalePlatform.Api.Middlewares
{
    public sealed class GlobalExceptionHandler : IExceptionHandler
    {
        private readonly ILogger<GlobalExceptionHandler> _logger;
        private readonly IServiceScopeFactory _scopeFactory;
        private readonly IStringLocalizer<Messages> _messages;
        private readonly IWebHostEnvironment _environment;

        public GlobalExceptionHandler(
            ILogger<GlobalExceptionHandler> logger,
            IServiceScopeFactory scopeFactory,
            IStringLocalizer<Messages> messages,
            IWebHostEnvironment environment)
        {
            _logger = logger;
            _scopeFactory = scopeFactory;
            _messages = messages;
            _environment = environment;
        }

        public async ValueTask<bool> TryHandleAsync(
            HttpContext context,
            Exception exception,
            CancellationToken cancellationToken)
        {
            var (statusCode, message, errors) = exception switch
            {
                ValidationException validationException =>
                    (
                        StatusCodes.Status400BadRequest,
                        _messages[ResourceCodes.ValidationFailed].Value,
                        (IReadOnlyList<string>)validationException.Errors
                            .Select(error => error.ErrorMessage)
                            .ToArray()
                    ),

                DuplicateRecordException =>
                    (
                        StatusCodes.Status409Conflict,
                        _messages[ResourceCodes.DuplicateRecord].Value,
                        (IReadOnlyList<string>)Array.Empty<string>()
                    ),

                _ =>
                    (
                        StatusCodes.Status500InternalServerError,
                        _messages[ResourceCodes.UnexpectedError].Value,
                        (IReadOnlyList<string>)Array.Empty<string>()
                    )
            };

            if (statusCode == StatusCodes.Status500InternalServerError)
            {
                // Log the original exception first. This remains available
                // even if writing to ExceptionLogs subsequently fails.
                _logger.LogError(
                    exception,
                    "Unhandled exception. TraceId: {TraceId}",
                    context.TraceIdentifier);

                await TryPersistExceptionAsync(context, exception);
            }

            context.Response.StatusCode = statusCode;

            var response = ApiResponse<object>.Failure(
                message,
                context.TraceIdentifier,
                errors);

            await context.Response.WriteAsJsonAsync(
                response,
                cancellationToken);

            return true;
        }

        private async Task TryPersistExceptionAsync(
            HttpContext context,
            Exception exception)
        {
            try
            {
                // IExceptionLogWriter and AppDbContext are scoped.
                await using var scope = _scopeFactory.CreateAsyncScope();

                var writer = scope.ServiceProvider
                    .GetRequiredService<IExceptionLogWriter>();

                var loginIdValue = context.User
                    .FindFirst(ClaimTypes.NameIdentifier)?.Value;

                Guid? loginId = Guid.TryParse(loginIdValue, out var parsedLoginId)
                    ? parsedLoginId
                    : null;

                var entry = new ExceptionLogEntry(
                    TraceId: context.TraceIdentifier,
                    LogLevel: "Error",
                    ExceptionType: exception.GetType().FullName,
                    Message: exception.Message,
                    StackTrace: exception.StackTrace,
                    InnerException: GetInnerExceptionDetails(exception),
                    RequestUrl: context.Request.Path.Value,
                    HttpMethod: context.Request.Method,
                    LoginId: loginId,
                    ClientIp: context.Connection.RemoteIpAddress?.ToString(),
                    UserAgent: context.Request.Headers.UserAgent.ToString(),
                    Environment: _environment.EnvironmentName,
                    Payload: await GetRequestPayloadAsync(context));

                // Do not use the request cancellation token for this
                // best-effort logging attempt.
                await writer.WriteAsync(entry, CancellationToken.None);
            }
            catch (Exception loggingException)
            {
                // Never let a secondary logging failure replace the
                // original exception.
                _logger.LogError(
                    loggingException,
                    "Failed to persist exception to ExceptionLogs. Original TraceId: {TraceId}",
                    context.TraceIdentifier);
            }
        }

        private static string? GetInnerExceptionDetails(Exception exception)
        {
            var details = new List<string>();

            for (var current = exception.InnerException;
                 current is not null;
                 current = current.InnerException)
            {
                details.Add(
                    $"{current.GetType().FullName}: {current.Message}");
            }

            return details.Count == 0
                ? null
                : string.Join(Environment.NewLine, details);
        }

        private static async Task<string?> GetRequestPayloadAsync(
    HttpContext context)
        {
            if (context.Request.ContentLength is null or 0 ||
                !context.Request.Body.CanRead)
            {
                return null;
            }

            context.Request.EnableBuffering();

            using var reader = new StreamReader(
                context.Request.Body,
                leaveOpen: true);

            var payload = await reader.ReadToEndAsync();

            context.Request.Body.Position = 0;

            return string.IsNullOrWhiteSpace(payload)
                ? null
                : payload;
        }
    }
}
