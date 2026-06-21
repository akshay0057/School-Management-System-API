using SchoolManagement.Domain.Entities.StudentModule;

namespace SchoolManagement.Infrastructure.Persistence.Repositories.Interfaces
{
    public interface IStudentRepository : IGenericRepository<StudentEntity>
    {
        Task<StudentEntity?> GetByAdmissionNoAsync(string admissionNo);
        Task<StudentEntity?> GetByRollNoAsync(string rollNo);
    }
}
