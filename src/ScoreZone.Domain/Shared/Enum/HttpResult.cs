namespace ScoreZone.Domain.Shared.Enum
{
    public enum HttpResult
    {
        Success = 200,
        Created = 201,
        Accepted = 202,
        NotContent = 204,

        //////
        
        BadRequest = 400,
        Unauthorized = 401,
        Forbidden = 403,
        NotFound = 404,
        MethodNotAllowed = 405,
        Conflict = 409,

        ////////
        
        ServerError = 500,
        BadGateway = 502
    }
}