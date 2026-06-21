using SchoolManagement.Application.Common.Models;
using SchoolManagement.Application.DTOs.Master;
using SchoolManagement.Application.DTOs.Menu;
using SchoolManagement.Application.DTOs.Module;
using SchoolManagement.Application.DTOs.Permission;
using SchoolManagement.Application.DTOs.RolePermission;

namespace SchoolManagement.Application.Interfaces
{
    public interface ISettingService
    {
        Task CreateAcademicSessionAsync(CreateAcademicSessionRequest request);
        Task CreateClassAsync(CreateClassRequest request);
        Task CreateSectionAsync(CreateSectionRequest request);
        Task CreateRoleAsync(CreateRoleRequest request);
        Task CreateModuleAsync(CreateModuleRequest request);
        Task CreatePermissionAsync(CreatePermissionRequest request);
        Task AssignPermissionsAsync(AssignRolePermissionRequest request);
        Task<ApiResponse<List<UserMenuResponse>>> GetUserMenuAsync(Guid userId);
        Task<ApiResponse<List<PermissionMatrixResponse>>>GetPermissionMatrixAsync();
        Task<ApiResponse<List<MenuTreeResponse>>>GetMenuTreeAsync();
        Task SavePermissionMatrixAsync(SavePermissionMatrixRequest request);
    }
}
