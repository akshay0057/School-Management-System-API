using SchoolManagement.Domain.Entities.AttendanceModule;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolManagement.Infrastructure.Persistence.Repositories.Interfaces
{
    public interface IStaffAttendanceRepository
    : IGenericRepository<StaffAttendanceEntity>
    {
        Task<bool> ExistsAttendanceAsync(
            Guid staffId,
            DateTime attendanceDate);
    }
}
