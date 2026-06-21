using Microsoft.EntityFrameworkCore;
using SchoolManagement.Domain.Entities.StudentModule;
using SchoolManagement.Infrastructure.Persistence.Repositories.Interfaces;

namespace SchoolManagement.Infrastructure.Persistence.Repositories.Implementations
{
    public class StudentRepository : GenericRepository<StudentEntity>, IStudentRepository
    {

        public StudentRepository(ApplicationDbContext dbContext) : base(dbContext)
        {
        }

        public async Task<StudentEntity?> GetByAdmissionNoAsync(string admissionNo)
        {
            return await _context.Students
                .FirstOrDefaultAsync(x => x.AdmissionNo == admissionNo);
        }

        public async Task<StudentEntity?> GetByRollNoAsync(string rollNo)
        {
            return await _context.Students
                .FirstOrDefaultAsync(x => x.RollNo == rollNo);
        }
    }
}
