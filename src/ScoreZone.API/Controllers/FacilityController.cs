using Microsoft.AspNetCore.Mvc;
using ScoreZone.Application.Facility.Interfaces;

namespace ScoreZone.API.Controllers
{
    public class FacilityController : ApiController
    {
        private readonly IFacilityService _service;

        public FacilityController(IFacilityService service)
        {
            _service = service;
        }


        [HttpGet("/{id}")]
        public async Task<IActionResult> GetById([FromRoute] Guid id)
        {
            var result = await _service.GetByIdAsync(id);

            return HandleResult(result);
        }

        
    }
}
        