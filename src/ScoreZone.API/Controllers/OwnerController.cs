using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ScoreZone.Application.Shared.DTOs;
using ScoreZone.Application.User.Owner.DTOs;
using ScoreZone.Application.User.Owner.Interfaces;
using ScoreZone.Domain.Shared.Enum;

namespace ScoreZone.API.Controllers
{
    public class OwnerController : ApiController
    {
        private readonly IOwnerService _service;

        public OwnerController(IOwnerService service)
        {
            _service = service;
        }


        [HttpGet]
        [Authorize(Roles = "Admin")]
        [EndpointSummary("Get All Owners")]
        [EndpointDescription("Get All Active Owners With Full Details")]
        [ProducesResponseType(typeof(PaginatedResultDto<OwnerDetailsResponse>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetAll([FromQuery] int pageNumber = 1, [FromQuery] int pageSize = 10)
        {
            var result = await _service.GetAllAsync(pageNumber, pageSize);

            return HandleResult(result);
        }


        [HttpGet("get-all-short")]
        [Authorize(Roles = "Admin")]
        [EndpointSummary("Get Owners")]
        [EndpointDescription("Get All Active Owners In Short Form")]
        [ProducesResponseType(typeof(IReadOnlyCollection<OwnerShortResponse>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetAllShort()
        {
            var result = await _service.GetAllShortAsync();
            return HandleResult(result);
        }


        [HttpGet("{id}")]
        [Authorize(Roles = "Admin")]
        [EndpointSummary("Owner Details")]
        [EndpointDescription("Get Owner Details By Id")]
        [ProducesResponseType(typeof(OwnerDetailsResponse), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetById([FromRoute] Guid id)
        {
            var result = await _service.GetByIdAsync(id);

            return HandleResult(result);
        }

        
    }
}
        