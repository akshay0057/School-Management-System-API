using SchoolManagement.Application.Common.Models;
using SchoolManagement.Application.DTOs.Student;

namespace SchoolManagement.Application.Interfaces
{
    public interface IStudentService
    {
        Task<ApiResponse<StudentResponse>> CreateAsync(AddStudentRequest request);
        Task<ApiResponse<StudentResponse>> UpdateAsync(UpdateStudentRequest request);
        Task<ApiResponse<bool>> DeleteAsync(Guid id);
        Task<ApiResponse<StudentResponse?>> GetByIdAsync(Guid id);
        Task<ApiResponse<PaginationResponse<StudentResponse>>>GetAllAsync(GetStudentListRequest request);
    }
}
