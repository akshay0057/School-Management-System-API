using SchoolManagement.Application.DTOs.Dashboard;
using SchoolManagement.Application.Interfaces;
using SchoolManagement.Infrastructure.Persistence;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using SchoolManagement.Application.Common.Models;

namespace SchoolManagement.Application.Services
{
    public sealed class DashboardService : IDashboardService
    {
        private readonly ApplicationDbContext _context;

        public DashboardService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<ApiResponse<DashboardSummaryResponse>> GetSummaryAsync()
        {
            var today = DateTime.UtcNow.Date;

            var currentMonth = today.Month;

            var currentYear = today.Year;

            var result = new DashboardSummaryResponse
            {
                TotalStudents =
                    await _context.Students
                        .CountAsync(x => x.IsActive),

                TotalStaff =
                    await _context.Staffs
                        .CountAsync(x => x.IsActive),

                TotalClasses =
                    await _context.Classes
                        .CountAsync(x => x.IsActive),

                TotalSections =
                    await _context.Sections
                        .CountAsync(x => x.IsActive),

                TodayStudentAttendance =
                    await _context.StudentAttendances
                        .CountAsync(x =>
                            x.AttendanceDate.Date == today
                            &&
                            x.Status == "Present"),

                TodayStaffAttendance =
                    await _context.StaffAttendances
                        .CountAsync(x =>
                            x.AttendanceDate.Date == today
                            &&
                            x.Status == "Present"),

                TodayCollection =
                    await _context.FeeCollections
                        .Where(x =>
                            x.PaymentDate.Date == today)
                        .SumAsync(x =>
                            (decimal?)x.Amount)
                        ?? 0,

                MonthlyCollection =
                    await _context.FeeCollections
                        .Where(x =>
                            x.PaymentDate.Month == currentMonth
                            &&
                            x.PaymentDate.Year == currentYear)
                        .SumAsync(x =>
                            (decimal?)x.Amount)
                        ?? 0,

                PendingFeeStudents =
                    await GetPendingFeeStudentsAsync()
            };

            return ApiResponse<DashboardSummaryResponse>.SuccessResponse(result);
        }

        #region Private Methods
        private async Task<int> GetPendingFeeStudentsAsync()
        {
            var currentMonth = DateTime.UtcNow.Month;

            var currentYear = DateTime.UtcNow.Year;

            var paidStudentIds =
                await _context.FeeCollections
                    .Where(x =>
                        x.Month == currentMonth
                        &&
                        x.Year == currentYear)
                    .Select(x => x.StudentId)
                    .Distinct()
                    .ToListAsync();

            return await _context.Students
                .CountAsync(x =>
                    x.IsActive
                    &&
                    !paidStudentIds.Contains(x.Id));
        }
    #endregion
    }
}
