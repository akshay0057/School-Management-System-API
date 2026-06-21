using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolManagement.Domain.Entities.SecurityModule
{
    public class RoleEntity : BaseEntity
    {
        public string Name { get; set; } = null!;
        public string? Description { get; set; }

        public ICollection<UserRoleEntity> UserRoles { get; set; } = [];
        public ICollection<RolePermissionEntity> RolePermissions { get; set; } = [];
    }
}
