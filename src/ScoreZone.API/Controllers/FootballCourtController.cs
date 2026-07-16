using ScoreZone.Application.Auth;
using Microsoft.AspNetCore.Mvc;
using ScoreZone.Application.FootballCourt.Interfaces;
using ScoreZone.Application.Facility.Interfaces;
using ScoreZone.Application.FootballCourt.DTOs;

namespace ScoreZone.API.Controllers
{
    public class FootballCourtController : ApiController
    {
        private readonly IFootballCourtService _service;

        public FootballCourtController(IFootballCourtService service)
        {
            _service = service;
        }


        [HttpGet("/{id}")]
        public async Task<IActionResult> GetById([FromRoute] Guid id)
        {
            var result = await _service.GetByIdAsync(id);

            return HandleResult(result);
        }

        [HttpPost("/browse-nearby-courts")]
        public async Task<IActionResult> BrowseNearbyCourts([FromBody] LocationCoordsRequest request)
        {
            var result = await _service.BrowseNearbyCourtsAsync(request);

            return HandleResult(result);
        }

        
    }
}
        