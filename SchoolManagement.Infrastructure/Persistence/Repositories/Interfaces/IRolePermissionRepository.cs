using SchoolManagement.Domain.Entities.SecurityModule;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolManagement.Infrastructure.Persistence.Repositories.Interfaces
{
    public interface IRolePermissionRepository : IGenericRepository<RolePermissionEntity>
    {
        Task<List<RolePermissionEntity>>GetByRoleIdAsync(Guid roleId);

        Task DeleteRangeAsync(List<RolePermissionEntity> entities);
    }
}
