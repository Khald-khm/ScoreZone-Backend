namespace ScoreZone.Domain.Shared.Exceptions
{
    public class NotFoundException : Exception
    {
        public int StatusCode {get;}
        public NotFoundException(int statusCode, string message = "Resource Not Found.") : base(message) 
        { 
            StatusCode = statusCode;
        }
    }
}