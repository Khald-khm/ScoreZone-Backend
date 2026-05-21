using FluentValidation;
using Microsoft.Extensions.DependencyInjection;
using ScoreZone.Application.Shared.Results;
using ScoreZone.Domain.Shared.Exceptions;

namespace ScoreZone.Application.Shared.Services
{
    
    public abstract class BaseApplicationService
    {
        private readonly IServiceProvider _serviceProvider;


        public BaseApplicationService(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }
        
        // WITH TData & TRequest
        protected async Task<AppResult<TData>> ExecuteAsync<TRequest, TData>(TRequest request, Func<Task<TData>> businessLogic, int statusCode = 200)
        {

            var validator = _serviceProvider.GetService<IValidator<TRequest>>();

            if(validator is not null)
            {
                var validatorResult = await validator.ValidateAsync(request);

                if(!validatorResult.IsValid)
                    return Result.ValidationError(validatorResult.Errors);
            }
            

            try
            {
                var data = await businessLogic();

                return Result.Success(data, statusCode, "Success");
            }
            catch(DomainException ex)
            {
                return Result.DomainError(ex.StatusCode, ex.Message);
            }
            // catch(NotFoundException ex)
            // {
            //     return Result.NotFound(ex.StatusCode, ex.Message);
            // }
            // catch(ForbiddenException ex)
            // {
            //     return Result.Forbidden(ex.StatusCode, ex.Message);
            // }
            catch(AppException ex)
            {
                return Result.ApplicationError(ex.StatusCode, ex.Message);
            }
            catch(Exception)
            {
                throw;
            }
        }


        // WITHOUT TRequest

        protected async Task<AppResult<TData>> ExecuteAsync<TData>(Func<Task<TData>> businessLogic, int statusCode = 200)
        {            

            try
            {
                var data = await businessLogic();

                return Result.Success(data, 200, "Success");
            }
            catch(DomainException ex)
            {
                return Result.DomainError(ex.StatusCode, ex.Message);
            }
            // catch(NotFoundException ex)
            // {
            //     return Result.NotFound(ex.StatusCode, ex.Message);
            // }
            // catch(ForbiddenException ex)
            // {
            //     return Result.Forbidden(ex.StatusCode, ex.Message);
            // }
            catch(AppException ex)
            {
                return Result.ApplicationError(ex.StatusCode, ex.Message);
            }
            catch(Exception)
            {
                throw;
            }
        }

        // WITHOUT TData

        protected async Task<AppResult> ExecuteAsync<TRequest>(TRequest request, Func<Task> businessLogic, int statusCode = 200)
        {

            var validator = _serviceProvider.GetService<IValidator<TRequest>>();

            if(validator is not null)
            {
                var validatorResult = await validator.ValidateAsync(request);

                if(!validatorResult.IsValid)
                    return Result.ValidationError(validatorResult.Errors);
            }
            
            
            try
            {
                await businessLogic();

                return Result.Success(statusCode,"Success");

            }

            catch(DomainException ex)
            {
                return Result.DomainError(ex.StatusCode, ex.Message);
            }

            catch(AppException ex)
            {
                return Result.ApplicationError(ex.StatusCode, ex.Message);
            }

            catch(Exception)
            {
                throw;
            }
        }
    }
}