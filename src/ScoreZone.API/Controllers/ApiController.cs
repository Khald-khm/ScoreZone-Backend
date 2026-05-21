using ScoreZone.Application.Shared.Results;
using ScoreZone.Domain.Shared.Enum;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.RateLimiting;

namespace ScoreZone.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Produces("application/json")]
    [EnableRateLimiting("GlobalFallback")]
    public abstract class ApiController : ControllerBase
    {

        protected IActionResult HandleResult(AppResult result)
        {
            return result.Match(
                // 200 - 201 - 202 - 204
                success => StatusCode(success.StatusCode, new
                {
                    type = Enum.GetName(typeof(HttpResult), success.StatusCode),
                    message = success.Message
                }),

                validationError => StatusCode( StatusCodes.Status400BadRequest, new
                {
                    type = "Validation Error",
                    message = validationError.errors.Select(e => new { field = e.PropertyName, error = e.ErrorMessage})
                }),

                domainError => StatusCode(domainError.StatusCode, new
                {
                    type = Enum.GetName(typeof(HttpResult), domainError.StatusCode),
                    message = domainError.Message
                }),

                applicationError => StatusCode(applicationError.StatusCode, new
                {
                    type = Enum.GetName(typeof(HttpResult), applicationError.StatusCode),
                    message = applicationError.Message
                })
            );
        }


        protected IActionResult HandleResult<TData>(AppResult<TData> result)
        {
            return result.Match(
                // 200 - 201 - 202
                data => StatusCode(data.StatusCode, new
                {
                    type = Enum.GetName(typeof(HttpResult), data.StatusCode),
                    message = data.Message,
                    datat = data.Data
                }),

                success => StatusCode(success.StatusCode, new
                {
                    type = Enum.GetName(typeof(HttpResult), success.StatusCode),
                    message = success.Message
                }),

                validationError => StatusCode( StatusCodes.Status400BadRequest, new
                {
                    type = "Validation Error",
                    message = validationError.errors.Select(e => new { field = e.PropertyName, error = e.ErrorMessage})
                }),

                domainError => StatusCode(domainError.StatusCode, new
                {
                    type = Enum.GetName(typeof(HttpResult), domainError.StatusCode),
                    message = domainError.Message
                }),

                applicationError => StatusCode(applicationError.StatusCode, new
                {
                    type = Enum.GetName(typeof(HttpResult), applicationError.StatusCode),
                    message = applicationError.Message
                })
            );
        }
        
    }
}