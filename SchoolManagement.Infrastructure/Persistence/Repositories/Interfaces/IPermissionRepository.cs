using SchoolManagement.Domain.Entities.SecurityModule;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolManagement.Infrastructure.Persistence.Repositories.Interfaces
{
    public interface IPermissionRepository
    : IGenericRepository<PermissionEntity>
    {
        Task<List<PermissionEntity>>
            GetByIdsAsync(
                List<Guid> ids);
    }
}
