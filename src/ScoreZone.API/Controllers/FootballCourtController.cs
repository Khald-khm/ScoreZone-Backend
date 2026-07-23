using Microsoft.AspNetCore.Mvc;
using ScoreZone.Application.FootballCourt.Interfaces;
using ScoreZone.Application.FootballCourt.DTOs;
using ScoreZone.Application.Shared.DTOs;
using Microsoft.AspNetCore.Authorization;
using ScoreZone.Domain.Shared.Enum;

namespace ScoreZone.API.Controllers
{
    public class FootballCourtController : ApiController
    {
        private readonly IFootballCourtService _service;

        public FootballCourtController(IFootballCourtService service)
        {
            _service = service;
        }


        
        [HttpGet]
        [Authorize(Roles = "Admin, Player")]
        [EndpointSummary("Get All Football Courts")]
        [EndpointDescription("Get All Active Football Courts With Full Details")]
        [ProducesResponseType(typeof(PaginatedResultDto<FootballCourtDetailsDto>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetAll([FromQuery] int pageNumber = 1, [FromQuery] int pageSize = 10)
        {
            var result = await _service.GetAllAsync(pageNumber, pageSize);

            return HandleResult(result);
        }


        [HttpGet("/{id}")]
        [EndpointSummary("Court Details")]
        [EndpointDescription("Get Football Court Details By Id")]
        [ProducesResponseType(typeof(FootballCourtDetailsDto), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetById([FromRoute] Guid id)
        {
            var result = await _service.GetByIdAsync(id);

            return HandleResult(result);
        }
        

        [HttpPost("/browse-nearby-courts")]
        [EndpointSummary("Browse Courts")]
        [EndpointDescription("Browse NearBy Football Courts")]
        [ProducesResponseType(typeof(PaginatedResultDto<FootballCourtDetailsDto>), StatusCodes.Status200OK)]
        public async Task<IActionResult> BrowseNearbyCourts([FromBody] LocationCoordsRequest request, [FromQuery] int pageNumber = 1, [FromQuery] int pageSize = 10)
        {
            var result = await _service.BrowseNearbyCourtsAsync(request, pageNumber, pageSize);

            return HandleResult(result);
        }


        [HttpPut("{id}/{status}")]
        [Authorize(Roles = "Admin")]
        [EndpointSummary("Change Status")]
        [EndpointDescription("Admin Can Accept, Reject, Pend & Block Football Court")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<IActionResult> ChangeStatus([FromRoute] Guid id, [FromRoute] CourtStatus status)
        {
            var result = await _service.ChangeStatusAsync(id, status);

            return HandleResult(result);
        }

        
    }
}
        