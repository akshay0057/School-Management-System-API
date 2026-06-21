using Microsoft.EntityFrameworkCore;
using SchoolManagement.Domain.Entities.StaffModule;
using SchoolManagement.Infrastructure.Persistence.Repositories.Interfaces;

namespace SchoolManagement.Infrastructure.Persistence.Repositories.Implementations
{
    public class StaffRepository : GenericRepository<StaffEntity>, IStaffRepository
    {
        public StaffRepository(ApplicationDbContext context) : base(context)
        {
        }

        public async Task<StaffEntity?> GetByEmployeeCodeAsync(string employeeCode)
        {
            return await _context.Staffs
                .FirstOrDefaultAsync(x =>
                    x.EmployeeCode == employeeCode);
        }

        public async Task<StaffEntity?> GetByEmailAsync(string email)
        {
            return await _context.Staffs
                .FirstOrDefaultAsync(x =>
                    x.Email == email);
        }
    }
}
