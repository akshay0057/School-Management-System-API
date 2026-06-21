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
    public class RoleRepository
    : GenericRepository<RoleEntity>,
      IRoleRepository
    {
        public RoleRepository(
            ApplicationDbContext context)
            : base(context)
        {
        }

        public async Task<List<RoleEntity>> GetRolesByIdsAsync(
            List<Guid> roleIds)
        {
            return await _context.Roles
                .Where(x => roleIds.Contains(x.Id))
                .ToListAsync();
        }

        public async Task<List<RoleEntity>>GetRolesWithPermissionsAsync()
        {
            return await _context.Roles

                .Include(x => x.RolePermissions)

                .ThenInclude(x => x.Permission)

                .ToListAsync();
        }
    }
}
