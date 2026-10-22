
using FluentValidation;

namespace Sansheera.WholesalePlatform.Api.Middlewares
{
    public sealed class ValidationExceptionMiddleware
    {
        private readonly RequestDelegate _next;

        public ValidationExceptionMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (ValidationException exception)
            {
                context.Response.StatusCode =
                    StatusCodes.Status400BadRequest;

                await context.Response.WriteAsJsonAsync(
                    new
                    {
                        errors = exception.Errors.Select(error => new
                        {
                            field = error.PropertyName,
                            message = error.ErrorMessage
                        })
                    },
                    context.RequestAborted);
            }
        }
    }
}
//note
//Ye middleware validation errors handle karta hai. Baaki unhandled exceptions ke liye production-grade global exception handler aur logging bhi configure karna.