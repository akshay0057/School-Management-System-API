using SchoolManagement.Application.Common.Models;
using SchoolManagement.Application.DTOs.Staff;

namespace SchoolManagement.Application.Interfaces
{
    public interface IStaffService
    {
        Task<ApiResponse<StaffResponse>> AddStaffAsync(AddStaffRequest request);

        Task<ApiResponse<StaffResponse>> UpdateStaffAsync(UpdateStaffRequest request);

        Task<ApiResponse<StaffResponse>> GetByIdAsync(Guid id);

        Task<ApiResponse<bool>> DeleteAsync(Guid id);

        Task<ApiResponse<PaginationResponse<StaffResponse>>>GetListAsync(GetStaffListRequest request);
    }
}
