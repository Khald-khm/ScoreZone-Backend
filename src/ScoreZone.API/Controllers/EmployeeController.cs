using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using ScoreZone.Application.User.Employee.DTOs;
using ScoreZone.Application.User.Employee.Interfaces;
using System.Net.Mime;
using ScoreZone.Application.Shared.DTOs;

namespace ScoreZone.API.Controllers
{
    public class EmployeeController : ApiController
    {
        private readonly IEmployeeService _service;

        public EmployeeController(IEmployeeService service)
        {
            _service = service;
        }

        
        [HttpPut("update")]
        [Authorize(Roles = "Employee, Owner, Admin")]
        [EndpointSummary("Update Profile")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [Consumes(MediaTypeNames.Multipart.FormData)]
        public async Task<IActionResult> Update([FromQuery] Guid? id, [FromForm] UpdateEmployeeRequest request)
        {
            var result = await _service.UpdateAsync(id, request);

            return HandleResult(result);
        }
        

        [HttpPut("delete")]
        [Authorize(Roles = "Employee, Owner, Admin")]
        [EndpointSummary("Delete Profile")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<IActionResult> Delete([FromQuery] Guid? id)
        {
            var result = await _service.DeleteAsync(id);

            return HandleResult(result);
        }


        // [HttpGet]
        // [Authorize(Roles = "Admin")]
        // [EndpointSummary("Get All Employees")]
        // [EndpointDescription("Get All Active Employees With Full Details")]
        // [ProducesResponseType(typeof(PaginatedResultDto<EmployeeDetailsResponse>), StatusCodes.Status200OK)]
        // public async Task<IActionResult> GetAll([FromQuery] int pageNumber = 1, [FromQuery] int pageSize = 10)
        // {
        //     var result = await _service.GetAllAsync(pageNumber, pageSize);

        //     return HandleResult(result);
        // }


        // [HttpGet("get-all-short")]
        // [Authorize(Roles = "Admin")]
        // [EndpointSummary("Get Employees")]
        // [EndpointDescription("Get All Active Employees In Short Form")]
        // [ProducesResponseType(typeof(IReadOnlyCollection<EmployeeShortResponse>), StatusCodes.Status200OK)]
        // public async Task<IActionResult> GetAllShort()
        // {
        //     var result = await _service.GetAllShortAsync();
        //     return HandleResult(result);
        // }


        [HttpGet("details")]
        [Authorize(Roles = "Employee, Owner, Admin")]
        [EndpointSummary("Employee Details")]
        [EndpointDescription("Get Employee Details By Id")]
        [ProducesResponseType(typeof(EmployeeDetailsResponse), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetById([FromQuery] Guid? id)
        {
            var result = await _service.GetByIdAsync(id);

            return HandleResult(result);
        }


        [HttpGet("my-employees")]
        [Authorize(Roles = "Owner")]
        [EndpointSummary("My Employees")]
        [EndpointDescription("Owner Get His Employees.")]
        [ProducesResponseType(typeof(IReadOnlyList<EmployeeDetailsResponse>), StatusCodes.Status200OK)]
        public async Task<IActionResult> MyEmployees()
        {
            var result = await _service.MyEmployees();

            return HandleResult(result);
        }


        [HttpPost("add-court")]
        [Authorize(Roles = "Owner, Admin")]
        [EndpointSummary("Add Court")]
        [EndpointDescription("Owner Add Football Court To Employee Privileges.")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> AddCourt([FromQuery] Guid courtId, [FromQuery] Guid employeeId)
        {
            var resutl = await _service.AddCourtAsync(courtId, employeeId);

            return HandleResult(resutl);
        }

        
    }
}
        