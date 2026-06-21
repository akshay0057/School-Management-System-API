using SchoolManagement.Application.Common.Models;
using SchoolManagement.Application.DTOs.Dashboard;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolManagement.Application.Interfaces
{
    public interface IDashboardService
    {
        Task<ApiResponse<DashboardSummaryResponse>> GetSummaryAsync();
    }
}
