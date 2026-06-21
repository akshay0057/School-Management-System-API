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
    public class PermissionRepository
    : GenericRepository<PermissionEntity>,
      IPermissionRepository
    {
        public PermissionRepository(
            ApplicationDbContext context)
            : base(context)
        {
        }

        public async Task<List<PermissionEntity>>
            GetByIdsAsync(
                List<Guid> ids)
        {
            return await _context.Permissions
                .Where(x =>
                    ids.Contains(x.Id))
                .ToListAsync();
        }
    }
}
