using SchoolManagement.Domain.Entities.SecurityModule;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolManagement.Infrastructure.Persistence.Repositories.Interfaces
{
    public interface IModuleRepository : IGenericRepository<ModuleEntity>
    {
        Task<ModuleEntity?> GetByCodeAsync(string code);
        Task<List<ModuleEntity>>GetMenuModulesAsync();
    }
}
