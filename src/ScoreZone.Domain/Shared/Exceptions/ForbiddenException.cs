namespace ScoreZone.Domain.Shared.Exceptions
{
    public class ForbiddenException : Exception
    {
        public int StatusCode {get;}
        public ForbiddenException(int statusCode, string message = "You Don't Have Permission to Access This Resource.") : base(message) 
        { 
            StatusCode = statusCode;
        }
    }
}