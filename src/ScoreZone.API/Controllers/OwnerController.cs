using System.Net.Mime;
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

        
        [HttpPut("update")]
        [Authorize(Roles = "Owner, Admin")]
        [EndpointSummary("Update Profile")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [Consumes(MediaTypeNames.Multipart.FormData)]
        public async Task<IActionResult> Update([FromQuery] Guid? id, [FromForm] UpdateOwnerRequest request)
        {
            var result = await _service.UpdateAsync(id, request);

            return HandleResult(result);
        }
        

        [HttpPut("delete")]
        [Authorize(Roles = "Owner, Admin")]
        [EndpointSummary("Delete Profile")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<IActionResult> Delete([FromQuery] Guid? id)
        {
            var result = await _service.DeleteAsync(id);

            return HandleResult(result);
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


        [HttpGet("details")]
        [Authorize(Roles = "Admin, Owner")]
        [EndpointSummary("Owner Details")]
        [EndpointDescription("Get Owner Details By Id")]
        [ProducesResponseType(typeof(OwnerDetailsResponse), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetById([FromQuery] Guid? id)
        {
            var result = await _service.GetByIdAsync(id);

            return HandleResult(result);
        }

        
    }
}
        