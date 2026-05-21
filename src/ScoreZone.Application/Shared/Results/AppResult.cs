using OneOf;

namespace ScoreZone.Application.Shared.Results
{

    /// <summary>
    /// A unified container that holds every possible type of retruning result.
    /// This acts as a standard return type for all application services.
    /// </summary>
    /// <typeparam name="TData">
    /// TData is a possible of data type that could be returned.
    /// </typeparam>
    // public class AppResult<TData> : OneOfBase<TData, Success, ValidationError, Forbidden, NotFound, DomainError>
    // {|
    // public class AppResult<TData> : OneOfBase<TData, Success, ValidationError, Forbidden, NotFound, DomainError>
    // {
    public class AppResult<TData> : OneOfBase<Success<TData>, Success, ValidationError, DomainError, ApplicationError>
    {
        protected AppResult (OneOf<
            Success<TData>,
            Success,
            ValidationError,
            // Forbidden,
            // NotFound,
            DomainError,
            ApplicationError> input
        ) 
        : base(input) { }

        // public static implicit operator AppResult<TData>(TData _) => new (_);
        public static implicit operator AppResult<TData>(Success<TData> _) => new (_);

        public static implicit operator AppResult<TData>(Success _) => new (_); 

        public static implicit operator AppResult<TData>(ValidationError _) => new (_);
        
        public static implicit operator AppResult<TData>(DomainError _) => new (_);

        public static implicit operator AppResult<TData>(ApplicationError _) => new (_);

        // public static implicit operator AppResult<TData>(Forbidden _ ) => new (_);
        
        // public static implicit operator AppResult<TData>(NotFound _) => new (_);
        
    }


    // WITHOUT TData
    public class AppResult : OneOfBase<Success, ValidationError, DomainError, ApplicationError>
    {
        protected AppResult (OneOf<
            Success,
            ValidationError,
            // Forbidden,
            // NotFound,
            DomainError,
            ApplicationError> input
        ) 
        : base(input) { }

        // public static implicit operator AppResult<TData>(TData _) => new (_);

        public static implicit operator AppResult(Success _) => new (_); 

        public static implicit operator AppResult(ValidationError _) => new (_);
        
        public static implicit operator AppResult(DomainError _) => new (_);

        public static implicit operator AppResult(ApplicationError _) => new (_);

        // public static implicit operator AppResult<TData>(Forbidden _ ) => new (_);
        
        // public static implicit operator AppResult<TData>(NotFound _) => new (_);
        
    }
}