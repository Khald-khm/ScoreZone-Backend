using FluentValidation.Results;
using Microsoft.AspNetCore.Http;

namespace ScoreZone.Application.Shared.Results
{
    public record Success<TData>(
        TData Data,
        int StatusCode = 200,
        string Message = "Operation Success."
    );

    public record Success(
        int StatusCode = 200,
        string Message = "Operation Success."
    );

    public record DomainError(
        int StatusCode = 400,
        string Message = "Business Rules Error."
    );

    public record ValidationError(List<ValidationFailure> errors);

    // public record NotFound(
    //     int StatusCode = 404,
    //     string Message = "Resource Not Found."
    // );

    // public record Forbidden(
    //     int StatusCode = 403,
    //     string Message = "You Don't Have Permission To Access This Resource."
    // );
    
    // public record Unauthorized(string Message = "User Unauthorized.");

    public record ApplicationError(
        int StatusCode = 404,
        string Message = "Application Error"
    );
    


    public static class Result
    {
        public static Success<TData> Success<TData>(TData Data, int StatusCode, string Message) 
                => new Success<TData>(Data, StatusCode, Message);
            
        public static Success Success(int StatusCode, string Message)
                => new Success(StatusCode, Message);

        public static DomainError DomainError(int StatusCode, string Message) 
                => new DomainError(StatusCode, Message);
        
        public static ValidationError ValidationError(List<ValidationFailure> errors)
                => new ValidationError(errors);
        
        public static ValidationError ValidationError(string property, string error)
                => new ValidationError(new List<ValidationFailure>
                {
                    new ValidationFailure(property, error)
                });

        // public static NotFound NotFound(int StatusCode, string Message) 
        //         => new NotFound(StatusCode, Message);
        

        // public static Forbidden Forbidden(int StatusCode, string Message)
        //         => new Forbidden(StatusCode, Message);
        
        // public static Unauthorized Unauthorized(string Message)
        //         => new Unauthorized(Message);

        public static ApplicationError ApplicationError(int StatusCode, string Message)
                => new ApplicationError(StatusCode, Message);
        
    }
}