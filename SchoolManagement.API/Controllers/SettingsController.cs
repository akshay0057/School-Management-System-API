using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SchoolManagement.Application.Common.Models;
using SchoolManagement.Application.Constants;
using SchoolManagement.Application.DTOs.Master;
using SchoolManagement.Application.DTOs.Module;
using SchoolManagement.Application.DTOs.Permission;
using SchoolManagement.Application.DTOs.RolePermission;
using SchoolManagement.Application.Interfaces;
using SchoolManagement.Application.Services;

namespace SchoolManagement.API.Controllers
{
    [Route("api/settings")]
    [Authorize]
    [ApiController]
    public class SettingsController : ControllerBase
    {
        private readonly ISettingService _settingService;
        public SettingsController(ISettingService settingService)
        {
            _settingService = settingService;
        }

        [HttpPost("role")]
        public async Task<IActionResult> CreateRole([FromBody] CreateRoleRequest request)
        {
            await _settingService.CreateRoleAsync(request);

            return Ok(
                ApiResponse<string>
                    .SuccessResponse(
                        "Role created successfully."));
        }

        [HttpPost("academic-session")]
        public async Task<IActionResult> CreateAcademicSession(CreateAcademicSessionRequest request)
        {
            await _settingService.CreateAcademicSessionAsync(request);

            return Ok(
                ApiResponse<string>
                    .SuccessResponse(
                        "Academic Session created successfully."));
        }

        [HttpPost("class")]
        public async Task<IActionResult> CreateClass(CreateClassRequest request)
        {
            await _settingService.CreateClassAsync(request);

            return Ok(
                ApiResponse<string>
                    .SuccessResponse(
                        "Class created successfully."));
        }

        [HttpPost("section")]
        public async Task<IActionResult> CreateSection(CreateSectionRequest request)
        {
            await _settingService.CreateSectionAsync(request);

            return Ok(
                ApiResponse<string>
                    .SuccessResponse(
                        "Section created successfully."));
        }

        [HttpPost("module")]
        public async Task<IActionResult> CreateModule(CreateModuleRequest request)
        {
            await _settingService.CreateModuleAsync(request);

            return Ok(
                ApiResponse<string>
                    .SuccessResponse(
                        "Module created successfully."));
        }

        [HttpPost("permission")]
        public async Task<IActionResult> CreatePermission(CreatePermissionRequest request)
        {
            await _settingService.CreatePermissionAsync(request);

            return Ok(
                ApiResponse<string>
                    .SuccessResponse(
                        "Permission created successfully."));
        }

        [HttpPost("role-permissions")]
        public async Task<IActionResult> AssignPermissions(AssignRolePermissionRequest request)
        {
            await _settingService.AssignPermissionsAsync(request);

            return Ok(
                ApiResponse<string>
                    .SuccessResponse(
                        "Assign permissions successfully."));
        }

        [HttpGet("menu")]
        public async Task<IActionResult> GetMenu()
        {
            var userId = Guid.Parse(User.FindFirst(ClaimConstants.UserId)!.Value);

            var response = await _settingService.GetUserMenuAsync(userId);

            return Ok(response);
        }

        [HttpGet("permission-matrix")]
        public async Task<IActionResult>GetPermissionMatrix()
        {
            var result =await _settingService.GetPermissionMatrixAsync();

            return Ok(result);
        }

        [HttpGet("menu-tree")]
        public async Task<IActionResult>GetMenuTree()
        {
            var result = await _settingService.GetMenuTreeAsync();

            return Ok(result);
        }

        [HttpPost("permission-matrix")]
        public async Task<IActionResult>SavePermissionMatrix(SavePermissionMatrixRequest request)
        {
            await _settingService.SavePermissionMatrixAsync(request);

            return Ok("Permissions updated successfully.");
        }
    }
}
