using Microsoft.EntityFrameworkCore;
using SchoolManagement.Domain.Entities.AttendanceModule;
using SchoolManagement.Infrastructure.Persistence.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolManagement.Infrastructure.Persistence.Repositories.Implementations
{
    public class StaffAttendanceRepository
     : GenericRepository<StaffAttendanceEntity>,
       IStaffAttendanceRepository
    {
        public StaffAttendanceRepository(
            ApplicationDbContext context)
            : base(context)
        {
        }

        public async Task<bool> ExistsAttendanceAsync(
            Guid staffId,
            DateTime attendanceDate)
        {
            return await _context.StaffAttendances
                .AnyAsync(x =>
                    x.StaffId == staffId &&
                    x.AttendanceDate.Date == attendanceDate.Date);
        }
    }
}
