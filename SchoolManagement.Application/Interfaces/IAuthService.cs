using SchoolManagement.Application.Common.Models;
using SchoolManagement.Application.DTOs.Auth;
using SchoolManagement.Application.DTOs.Menu;
using SchoolManagement.Application.DTOs.Module;
using SchoolManagement.Application.DTOs.Permission;
using SchoolManagement.Application.DTOs.RolePermission;

namespace SchoolManagement.Application.Interfaces
{
    public interface IAuthService
    {
        Task<ApiResponse<LoginResponse>> LoginAsync(LoginRequest request);
        Task RegisterUserAsync(RegisterRequest request);
    }
}
