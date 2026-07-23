using System.Net.Mime;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ScoreZone.Application.Reservation.DTOs;
using ScoreZone.Application.Reservation.Interfaces;
using ScoreZone.Application.Shared.DTOs;
using ScoreZone.Domain.Shared.Enum;

namespace ScoreZone.API.Controllers
{
    public class ReservationController : ApiController
    {
        private readonly IReservationService _service;

        public ReservationController(IReservationService service)
        {
            _service = service;
        }
        

        [HttpPost]
        [EndpointSummary("Create Reservation")]
        [EndpointDescription("Player Make A New Reservation")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        public async Task<IActionResult> MakeReservation([FromBody] AddUpdateReservationRequest request)
        {
            var result = await _service.AddAsync(request);

            return HandleResult(result);
        }

        
        [HttpPut("{id}")]
        [EndpointSummary("Update Reservation")]
        [EndpointDescription("Player Update An Existing Reservation")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<IActionResult> UpdateReservation([FromRoute] Guid id, [FromBody] AddUpdateReservationRequest request)
        {
            var result = await _service.UpdateAsync(id, request);

            return HandleResult(result);
        }

        
        [HttpGet("{id}")]
        [EndpointSummary("Reservation Details")]
        [EndpointDescription("Get Reservation Details By Id")]
        [ProducesResponseType(typeof(ReservationDetails), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetDetailsById([FromRoute] Guid id)
        {
            var result = await _service.GetDetailsByIdAsync(id);

            return HandleResult(result);
        }

        
        [HttpPost("reserved-slots")]
        [EndpointSummary("Reserved Time Slots")]
        [EndpointDescription("Get All Reserved Time Slots For A Football Court In A Specific Date")]
        [ProducesResponseType(typeof(IReadOnlyList<ReservedSlots>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetReservedSlots([FromForm] ViewAvailableSlotsRequest request)
        {
            var result = await _service.GetReservedSlotsAsync(request);

            return HandleResult(result);
        }
        

        [HttpGet("my-reservations")]
        [Authorize(Roles = "Player")]
        [EndpointSummary("My Reservations")]
        [EndpointDescription("Player Gets All His Reservations")]
        [ProducesResponseType(typeof(PaginatedResultDto<MyReservation>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetMyReservations([FromQuery] int pageNumber = 1, [FromQuery] int pageSize = 10)
        {
            var result = await _service.GetMyReservationsAsync(pageNumber, pageSize);

            return HandleResult(result);
        }
        

        [HttpPatch("pay-deposite/{id}")]
        [Authorize(Roles = "Employee, Owner")]
        [EndpointSummary("Pay Deposite")]
        [EndpointDescription("Player Pays Reservation Deposite")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<IActionResult> PayDeposite([FromRoute] Guid id, [FromBody] PayDepositeRequest request)
        {
            var result = await _service.PayDepositeAsync(id, request);

            return HandleResult(result);
        }


    }
}
