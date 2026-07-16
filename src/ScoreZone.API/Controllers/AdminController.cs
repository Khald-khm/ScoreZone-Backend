using Microsoft.AspNetCore.Mvc;
using ScoreZone.Application.FootballCourt.Interfaces;
using Microsoft.AspNetCore.Authorization;
using ScoreZone.Application.Facility.DTOs;
using ScoreZone.Application.Facility.Interfaces;
using ScoreZone.Application.FootballCourt.DTOs;

namespace ScoreZone.API.Controllers
{
    [Authorize(Roles = "Admin")]
    public class AdminController : ApiController
    {
        private readonly IFootballCourtService _footballCourtService;
        private readonly IFacilityService _facilityService;

        public AdminController(IFootballCourtService footballCourtService, IFacilityService facilityService)
        {
            _footballCourtService = footballCourtService;
            _facilityService = facilityService;
        }

        [HttpPost("add-facility")]
        public async Task<IActionResult> AddFacility(AddFacilityRequest request)
        {
            var result = await _facilityService.AddAsync(request);

            return HandleResult(result);
        }
        
        [HttpPost("add-football-court")]
        public async Task<IActionResult> AddFootballCourt(AddFootballCourtRequest request)
        {
            var result = await _footballCourtService.AddAsync(request);

            return HandleResult(result);
        }
    }
}
        