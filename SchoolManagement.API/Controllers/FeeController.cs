using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SchoolManagement.Application.Common.Models;
using SchoolManagement.Application.DTOs.FeeCollection;
using SchoolManagement.Application.DTOs.FeeStructure;
using SchoolManagement.Application.Interfaces;

namespace SchoolManagement.API.Controllers
{
    [ApiController]
    [Route("api/fee")]
    [Authorize]
    public class FeeController : ControllerBase
    {
        private readonly IFeeService _feeService;

        public FeeController(
            IFeeService feeService)
        {
            _feeService = feeService;
        }

        [HttpPost("structure")]
        public async Task<IActionResult> CreateFeeStructure(CreateFeeStructureRequest request)
        {
            await _feeService.CreateFeeStructureAsync(request);

            return Ok(
                ApiResponse<string>
                    .SuccessResponse(
                        "Fee structure created."));
        }

        [HttpPost("collect")]
        public async Task<IActionResult> CollectFee(CreateFeeCollectionRequest request)
        {
            await _feeService.CollectFeeAsync(request);

            return Ok(
                ApiResponse<string>
                    .SuccessResponse(
                        "Fee collected successfully."));
        }

        [HttpGet("student-history/{studentId}")]
        public async Task<IActionResult> GetStudentHistory(Guid studentId)
        {
            var result = await _feeService.GetStudentFeeHistoryAsync(studentId);

            return Ok(result);
        }

        [HttpGet("due/{studentId}")]
        public async Task<IActionResult> GetDue(Guid studentId)
        {
            var result = await _feeService.GetStudentDueAsync(studentId);

            return Ok(result);
        }
    }
}
