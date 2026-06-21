using SchoolManagement.Application.DTOs.StaffAttendence;
using SchoolManagement.Application.DTOs.StudentAttendence;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolManagement.Application.Interfaces
{
    public interface IAttendanceService
    {
        Task CreateStudentAttendanceAsync(CreateStudentAttendanceRequest request);

        Task CreateBulkStudentAttendanceAsync(BulkStudentAttendanceRequest request);

        Task CreateStaffAttendanceAsync(CreateStaffAttendanceRequest request);
    }
}
