using Microsoft.EntityFrameworkCore;
using SchoolManagement.Domain.Entities.SecurityModule;
using SchoolManagement.Infrastructure.Persistence.Repositories.Interfaces;

namespace SchoolManagement.Infrastructure.Persistence.Repositories.Implementations
{
    public class UserRepository: GenericRepository<UserEntity>, IUserRepository
    {
        public UserRepository(ApplicationDbContext context)
            : base(context)
        {
        }

        public async Task<UserEntity?> GetUserByUsernameAsync(string username)
        {
            return await _context.Users
                .Include(x => x.UserRoles!)
                    .ThenInclude(x => x.Role!)
                        .ThenInclude(x => x.RolePermissions!)
                            .ThenInclude(x => x.Permission)
                .FirstOrDefaultAsync(x =>
                    x.Username == username &&
                    x.IsActive);
        }

        public async Task<UserEntity?>GetUserWithPermissionsAsync(Guid userId)
        {
            return await _context.Users
                .Include(x => x.UserRoles)
                    .ThenInclude(x => x.Role)
                        .ThenInclude(x => x.RolePermissions)
                            .ThenInclude(x => x.Permission)
                                .ThenInclude(x => x.Module)
                .FirstOrDefaultAsync(x =>
                    x.Id == userId);
        }

        public async Task<UserEntity?>GetUserWithCompletePermissionsAsync(Guid userId)
        {
            return await _context.Users
                .Include(x => x.UserRoles)
                    .ThenInclude(x => x.Role)
                        .ThenInclude(x => x.RolePermissions)
                            .ThenInclude(x => x.Permission)
                                .ThenInclude(x => x.Module)
                                    .ThenInclude(x => x.ParentModule)
                .FirstOrDefaultAsync(x =>
                    x.Id == userId);
        }
    }
}
