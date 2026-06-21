using SchoolManagement.Domain.Entities.StaffModule;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolManagement.Infrastructure.Persistence.Repositories.Interfaces
{
    public interface IStaffRepository : IGenericRepository<StaffEntity>
    {
        Task<StaffEntity?> GetByEmployeeCodeAsync(string employeeCode);
        Task<StaffEntity?> GetByEmailAsync(string email);
    }
}
