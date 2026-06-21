using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolManagement.Application.DTOs.Dashboard
{
    public sealed class DashboardSummaryResponse
    {
        public int TotalStudents { get; set; }

        public int TotalStaff { get; set; }

        public int TotalClasses { get; set; }

        public int TotalSections { get; set; }

        public int TodayStudentAttendance { get; set; }

        public int TodayStaffAttendance { get; set; }

        public decimal TodayCollection { get; set; }

        public decimal MonthlyCollection { get; set; }

        public int PendingFeeStudents { get; set; }
    }
}
