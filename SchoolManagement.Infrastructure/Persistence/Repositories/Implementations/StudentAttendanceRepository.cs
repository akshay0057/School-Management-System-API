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
    public class StudentAttendanceRepository : GenericRepository<StudentAttendanceEntity>, IStudentAttendanceRepository
    {
        public StudentAttendanceRepository(ApplicationDbContext context) : base(context)
        {
        }

        public async Task<bool> ExistsAttendanceAsync(Guid studentId, DateTime attendanceDate)
        {
            return await _context.StudentAttendances
                .AnyAsync(x =>
                    x.StudentId == studentId &&
                    x.AttendanceDate.Date == attendanceDate.Date);
        }

        public async Task<List<Guid>> GetAlreadyMarkedStudentIdsAsync(List<Guid> studentIds, DateTime attendanceDate)
        {
            return await _context.StudentAttendances
                .Where(x =>
                    studentIds.Contains(x.StudentId)
                    && x.AttendanceDate.Date == attendanceDate.Date)
                .Select(x => x.StudentId)
                .ToListAsync();
        }
    }
}
