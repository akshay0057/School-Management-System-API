using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SchoolManagement.API.Authorization;
using SchoolManagement.Application.DTOs.Student;
using SchoolManagement.Application.Interfaces;

namespace SchoolManagement.API.Controllers
{
    [Route("api/students")]
    [ApiController]
    [Authorize]
    public class StudentController : ControllerBase
    {
        private readonly IStudentService _studentService;

        public StudentController(IStudentService studentService)
        {
            _studentService = studentService;
        }

        [Permission("Student.Create")]
        [HttpPost("add")]
        public async Task<IActionResult> Create([FromBody] AddStudentRequest request)
        {
            return Ok(
                await _studentService.CreateAsync(request));
        }

        [Permission("Student.View")]
        [HttpGet]
        public async Task<IActionResult> GetAll([FromQuery]GetStudentListRequest request)
        {
            var response = await _studentService.GetAllAsync(request);
            return Ok(response);
        }

        [HttpGet("{id:guid}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            var response = await _studentService.GetByIdAsync(id);
            return Ok(response);
        }

        [HttpPut("update")]
        public async Task<IActionResult> Update(UpdateStudentRequest request)
        {
            var response = await _studentService.UpdateAsync(request);
            return Ok(response);
        }

        [Permission("Student.Delete")]
        [HttpDelete("delete/{id:guid}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            var response = await _studentService.DeleteAsync(id);
            return Ok(response);
        }
    }
}
