using SchoolManagement.Application.Common.Models;
using SchoolManagement.Application.DTOs.FeeCollection;
using SchoolManagement.Application.DTOs.FeeStructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolManagement.Application.Interfaces
{
    public interface IFeeService
    {
        Task CreateFeeStructureAsync(CreateFeeStructureRequest request);
        Task CollectFeeAsync(CreateFeeCollectionRequest request);
        Task<ApiResponse<StudentFeeHistoryResponse>>GetStudentFeeHistoryAsync(Guid studentId);
        Task<ApiResponse<FeeDueResponse>>GetStudentDueAsync(Guid studentId);
    }
}
