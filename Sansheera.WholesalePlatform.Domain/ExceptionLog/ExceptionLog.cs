using System.Net;

namespace Sansheera.WholesalePlatform.Domain.ExceptionLog
{
    public sealed class ExceptionLog
    {
        private ExceptionLog()
        {
            // EF Core
        }

        public ExceptionLog(
            string? traceId,
            string logLevel,
            string? exceptionType,
            string message,
            string? stackTrace,
            string? innerException,
            string? requestUrl,
            string? httpMethod,
            Guid? loginId,
            IPAddress? clientIp,
            string? userAgent,
            string? environment,
            string? payload)
        {
            Timestamp = DateTime.UtcNow;
            TraceId = traceId;
            LogLevel = logLevel;
            ExceptionType = exceptionType;
            Message = message;
            StackTrace = stackTrace;
            InnerException = innerException;
            RequestUrl = requestUrl;
            HttpMethod = httpMethod;
            LoginId = loginId;
            ClientIp = clientIp;
            UserAgent = userAgent;
            Environment = environment;
            Payload = payload;
        }

        public long Id { get; private set; }

        public DateTime Timestamp { get; private set; }

        public string? TraceId { get; private set; }

        public string LogLevel { get; private set; } = "Error";

        public string? ExceptionType { get; private set; }

        public string Message { get; private set; } = string.Empty;

        public string? StackTrace { get; private set; }

        public string? InnerException { get; private set; }

        public string? RequestUrl { get; private set; }

        public string? HttpMethod { get; private set; }

        public Guid? LoginId { get; private set; }

        public IPAddress? ClientIp { get; private set; }

        public string? UserAgent { get; private set; }

        public string? Environment { get; private set; }

        public string? Payload { get; private set; }
    }
}
