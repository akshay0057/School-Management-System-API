using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolManagement.Domain.Entities.SecurityModule
{
    public class PermissionEntity : BaseEntity
    {
        public Guid ModuleId { get; set; }
        public string Name { get; set; } = null!;
        public string? Code { get; set; }

        public ModuleEntity? Module { get; set; }
        public ICollection<RolePermissionEntity>? RolePermissions { get; set; } = [];
    }
}
