namespace ScoreZone.Domain.Shared.Exceptions
{
    public class DomainException : Exception
    {
        public int StatusCode {get;}
        public DomainException(int statusCode, string message = "Domain Validation Error.") : base(message) {}
    }
}