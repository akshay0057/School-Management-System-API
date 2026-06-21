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
    public class RolePermissionRepository
    : GenericRepository<RolePermissionEntity>,
      IRolePermissionRepository
    {
        public RolePermissionRepository(
            ApplicationDbContext context)
            : base(context)
        {
        }

        public async Task<List<RolePermissionEntity>>
    GetByRoleIdAsync(
        Guid roleId)
        {
            return await _context.RolePermissions
                .Where(x =>
                    x.RoleId == roleId)
                .ToListAsync();
        }

        public Task DeleteRangeAsync(
    List<RolePermissionEntity> entities)
        {
            _context.RolePermissions
                .RemoveRange(entities);

            return Task.CompletedTask;
        }
    }
}
