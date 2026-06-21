using SchoolManagement.Domain.Entities.AttendanceModule;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolManagement.Infrastructure.Persistence.Repositories.Interfaces
{
    public interface IStudentAttendanceRepository
    : IGenericRepository<StudentAttendanceEntity>
    {
        Task<bool> ExistsAttendanceAsync(Guid studentId, DateTime attendanceDate);
        Task<List<Guid>> GetAlreadyMarkedStudentIdsAsync(List<Guid> studentIds, DateTime attendanceDate);
    }
}
