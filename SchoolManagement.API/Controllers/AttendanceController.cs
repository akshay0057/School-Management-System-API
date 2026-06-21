using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SchoolManagement.Application.DTOs.StaffAttendence;
using SchoolManagement.Application.DTOs.StudentAttendence;
using SchoolManagement.Application.Interfaces;

namespace SchoolManagement.API.Controllers
{
    [Route("api/attendance")]
    [ApiController]
    public class AttendanceController : ControllerBase
    {
        private readonly IAttendanceService _attendanceService;

        public AttendanceController(
            IAttendanceService attendanceService)
        {
            _attendanceService = attendanceService;
        }

        [HttpPost("student")]
        public async Task<IActionResult> MarkStudentAttendance([FromBody] CreateStudentAttendanceRequest request)
        {
            await _attendanceService
                .CreateStudentAttendanceAsync(request);

            return Ok("Attendance marked successfully.");
        }

        [HttpPost("student/bulk")]
        public async Task<IActionResult> MarkBulkStudentAttendance([FromBody]  BulkStudentAttendanceRequest request)
        {
            await _attendanceService
                .CreateBulkStudentAttendanceAsync(request);

            return Ok("Attendance saved successfully.");
        }

        [HttpPost("staff")]
        public async Task<IActionResult> MarkStaffAttendance([FromBody] CreateStaffAttendanceRequest request)
        {
            await _attendanceService
                .CreateStaffAttendanceAsync(request);

            return Ok("Attendance marked successfully.");
        }
    }
}
