using SchoolManagement.Domain.Entities.SecurityModule;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolManagement.Infrastructure.Persistence.Repositories.Interfaces
{
    public interface IUserRepository : IGenericRepository<UserEntity>
    {
        Task<UserEntity?> GetUserByUsernameAsync(string username);
        Task<UserEntity?>GetUserWithPermissionsAsync(Guid userId);
        Task<UserEntity?>GetUserWithCompletePermissionsAsync(Guid userId);
    }
}
