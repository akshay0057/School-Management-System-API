using Microsoft.EntityFrameworkCore;
using SchoolManagement.Domain.Entities.SecurityModule;
using SchoolManagement.Infrastructure.Persistence.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolManagement.Infrastructure.Persistence.Repositories.Implementations
{
    public class ModuleRepository
    : GenericRepository<ModuleEntity>,
      IModuleRepository
    {
        public ModuleRepository(
            ApplicationDbContext context)
            : base(context)
        {
        }

        public async Task<ModuleEntity?>
            GetByCodeAsync(
                string code)
        {
            return await _context.Modules
                .FirstOrDefaultAsync(x =>
                    x.Code == code);
        }

        public async Task<List<ModuleEntity>>
    GetMenuModulesAsync()
        {
            return await _context.Modules

                .Include(x => x.ChildModules)

                .OrderBy(x =>
                    x.DisplayOrder)

                .ToListAsync();
        }
    }
}
