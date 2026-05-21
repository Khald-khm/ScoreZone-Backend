namespace ScoreZone.Domain.Shared.Exceptions
{
    public class AppException : Exception
    {
        public int StatusCode { get; }

        public AppException(int statusCode, string message = "Application Error.") : base(message) { }
    }
}