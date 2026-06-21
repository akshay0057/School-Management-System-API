using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
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

            return Ok("Role created successfully.");
        }

        [HttpPost("academic-session")]
        public async Task<IActionResult> CreateAcademicSession(CreateAcademicSessionRequest request)
        {
            await _settingService.CreateAcademicSessionAsync(request);

            return Ok("Academic Session created successfully.");
        }

        [HttpPost("class")]
        public async Task<IActionResult> CreateClass(CreateClassRequest request)
        {
            await _settingService.CreateClassAsync(request);

            return Ok("Class created successfully.");
        }

        [HttpPost("section")]
        public async Task<IActionResult> CreateSection(CreateSectionRequest request)
        {
            await _settingService.CreateSectionAsync(request);

            return Ok("Section created successfully.");
        }

        [HttpPost("module")]
        public async Task<IActionResult> CreateModule(CreateModuleRequest request)
        {
            await _settingService.CreateModuleAsync(request);

            return Ok("Module created successfully.");
        }

        [HttpPost("permission")]
        [Authorize]
        public async Task<IActionResult> CreatePermission(CreatePermissionRequest request)
        {
            await _settingService.CreatePermissionAsync(request);

            return Ok("Permission created successfully.");
        }

        [HttpPost("role-permissions")]
        [Authorize]
        public async Task<IActionResult> AssignPermissions(AssignRolePermissionRequest request)
        {
            await _settingService.AssignPermissionsAsync(request);

            return Ok("Permissions assigned successfully.");
        }

        [HttpGet("menu")]
        [Authorize]
        public async Task<IActionResult> GetMenu()
        {
            var userId = Guid.Parse(User.FindFirst(ClaimConstants.UserId)!.Value);

            var menu = await _settingService.GetUserMenuAsync(userId);

            return Ok(menu);
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
