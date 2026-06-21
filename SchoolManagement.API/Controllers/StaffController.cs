using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SchoolManagement.Application.DTOs.Staff;
using SchoolManagement.Application.Interfaces;

namespace SchoolManagement.API.Controllers
{
    [Route("api/staffs")]
    [ApiController]
    [Authorize]
    public class StaffController : ControllerBase
    {
        private readonly IStaffService _staffService;

        public StaffController(
            IStaffService staffService)
        {
            _staffService = staffService;
        }

        [HttpPost("add")]
        public async Task<IActionResult> AddStaff([FromBody] AddStaffRequest request)
        {
            var result = await _staffService.AddStaffAsync(request);

            return Ok(result);
        }

        [HttpPut("update")]
        public async Task<IActionResult> UpdateStaff([FromBody] UpdateStaffRequest request)
        {
            var result = await _staffService.UpdateStaffAsync(request);

            return Ok(result);
        }

        [HttpGet("{id:guid}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            var result = await _staffService.GetByIdAsync(id);

            return Ok(result);
        }

        [HttpDelete("{id:guid}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            var result = await _staffService.DeleteAsync(id);

            return Ok(result);
        }

        [HttpGet]
        public async Task<IActionResult> GetList([FromQuery] GetStaffListRequest request)
        {
            var result = await _staffService.GetListAsync(request);

            return Ok(result);
        }
    }
}
