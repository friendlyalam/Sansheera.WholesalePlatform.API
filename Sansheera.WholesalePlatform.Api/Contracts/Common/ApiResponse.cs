namespace Sansheera.WholesalePlatform.Api.Contracts.Common
{
    public sealed record ApiResponse<T>(
    bool IsSuccess,
    string Message,
    T? Result,
    IReadOnlyList<string> Errors,
    string TraceId)
    {
        public static ApiResponse<T> Success(
            T result,
            string message,
            string traceId) =>
            new(true, message, result, Array.Empty<string>(), traceId);

        public static ApiResponse<T> Failure(
            string message,
            string traceId,
            IReadOnlyList<string>? errors = null) =>
            new(false, message, default, errors ?? Array.Empty<string>(), traceId);
    }
}
