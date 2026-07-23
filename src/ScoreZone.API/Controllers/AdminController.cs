using Microsoft.AspNetCore.Mvc;
using ScoreZone.Application.FootballCourt.Interfaces;
using Microsoft.AspNetCore.Authorization;
using ScoreZone.Application.Facility.DTOs;
using ScoreZone.Application.Facility.Interfaces;
using ScoreZone.Application.FootballCourt.DTOs;
using System.Net.Mime;
using ScoreZone.Domain.Shared.Enum;

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
        [Authorize(Roles = "Admin")]
        [EndpointSummary("Create Facility")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [Consumes(MediaTypeNames.Multipart.FormData)]
        public async Task<IActionResult> AddFacility([FromForm] AddFacilityRequest request)
        {
            var result = await _facilityService.AddAsync(request);

            return HandleResult(result);
        }

        
        [HttpPost("add-football-court")]
        [Authorize(Roles = "Admin")]
        [EndpointSummary("Create Football Court")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [Consumes(MediaTypeNames.Multipart.FormData)]
        public async Task<IActionResult> AddFootballCourt([FromForm] AddFootballCourtRequest request)
        {
            var result = await _footballCourtService.AddAsync(request);

            return HandleResult(result);
        }
    }
}
        