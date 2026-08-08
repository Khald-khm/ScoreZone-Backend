using System.Net.Mime;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ScoreZone.Application.Shared.DTOs;
using ScoreZone.Application.User.Player.DTOs;
using ScoreZone.Application.User.Player.Interfaces;

namespace ScoreZone.API.Controllers
{
    public class PlayerController : ApiController
    {
        private readonly IPlayerService _service;

        public PlayerController(IPlayerService service)
        {
            _service = service;
        }

        
        [HttpPut("update")]
        [Authorize(Roles = "Player, Admin")]
        [EndpointSummary("Update Profile")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [Consumes(MediaTypeNames.Multipart.FormData)]
        public async Task<IActionResult> Update([FromQuery] Guid? id, [FromForm] UpdatePlayerRequest request)
        {
            var result = await _service.UpdateAsync(id, request);

            return HandleResult(result);
        }
        

        [HttpPut("delete")]
        [Authorize(Roles = "Player, Admin")]
        [EndpointSummary("Delete Profile")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<IActionResult> Delete([FromQuery] Guid? id)
        {
            var result = await _service.DeleteAsync(id);

            return HandleResult(result);
        }


        // [HttpGet]
        // [Authorize(Roles = "Admin")]
        // [EndpointSummary("Get All Players")]
        // [EndpointDescription("Get All Active Players With Full Details")]
        // [ProducesResponseType(typeof(PaginatedResultDto<PlayerDetailsResponse>), StatusCodes.Status200OK)]
        // public async Task<IActionResult> GetAll([FromQuery] int pageNumber = 1, [FromQuery] int pageSize = 10)
        // {
        //     var result = await _service.GetAllAsync(pageNumber, pageSize);

        //     return HandleResult(result);
        // }


        // [HttpGet("get-all-short")]
        // [Authorize(Roles = "Admin")]
        // [EndpointSummary("Get Players")]
        // [EndpointDescription("Get All Active Players In Short Form")]
        // [ProducesResponseType(typeof(IReadOnlyCollection<PlayerShortResponse>), StatusCodes.Status200OK)]
        // public async Task<IActionResult> GetAllShort()
        // {
        //     var result = await _service.GetAllShortAsync();
        //     return HandleResult(result);
        // }


        [HttpGet("details")]
        [Authorize(Roles = "Admin, Player")]
        [EndpointSummary("Player Details")]
        [EndpointDescription("Get Player Details By Id")]
        [ProducesResponseType(typeof(PlayerDetailsResponse), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetById([FromQuery] Guid? id)
        {
            var result = await _service.GetByIdAsync(id);

            return HandleResult(result);
        }

        
    }
}
        