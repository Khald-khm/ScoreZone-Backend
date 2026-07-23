using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ScoreZone.Application.Facility.Interfaces;
using ScoreZone.Application.Shared.DTOs;
using ScoreZone.Domain.Shared.Enum;

namespace ScoreZone.API.Controllers
{
    public class FacilityController : ApiController
    {
        private readonly IFacilityService _service;

        public FacilityController(IFacilityService service)
        {
            _service = service;
        }


        [HttpGet]
        [Authorize(Roles = "Admin, Player")]
        [EndpointSummary("Get All Facilities")]
        [EndpointDescription("Get All Active Facilities With Full Details")]
        [ProducesResponseType(typeof(PaginatedResultDto<FacilityDetailsDto>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetAll([FromQuery] int pageNumber = 1, [FromQuery] int pageSize = 10)
        {
            var result = await _service.GetAllAsync(pageNumber, pageSize);

            return HandleResult(result);
        }


        [HttpGet("get-all-short")]
        [Authorize(Roles = "Admin, Player")]
        [EndpointSummary("Get Facilities")]
        [EndpointDescription("Get All Active Facilities In Short Form")]
        [ProducesResponseType(typeof(IReadOnlyCollection<FacilityShortDto>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetAllShort()
        {
            var result = await _service.GetAllShortAsync();

            return HandleResult(result);
        }


        [HttpGet("{id}")]
        [Authorize(Roles = "Admin, Player")]
        [EndpointSummary("Facility Details")]
        [EndpointDescription("Get Facility Details By Id")]
        [ProducesResponseType(typeof(FacilityDetailsDto), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetById([FromRoute] Guid id)
        {
            var result = await _service.GetByIdAsync(id);

            return HandleResult(result);
        }


        [HttpPut("{id}/{status}")]
        [Authorize(Roles = "Admin")]
        [EndpointSummary("Change Status")]
        [EndpointDescription("Admin Can Accept, Reject, Pend & Block Facility")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<IActionResult> ChangeStatus([FromRoute] Guid id, [FromRoute] FacilityStatus status)
        {
            var result = await _service.ChangeStatusAsync(id, status);

            return HandleResult(result);
        }
        
    }
}
        