using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolManagement.Domain.Entities.SecurityModule
{
    public class ModuleEntity : BaseEntity
    {
        public string Name { get; set; } = null!;
        public string? Code { get; set; }
        public string? Route { get; set; }
        public string? Icon { get; set; }
        public int DisplayOrder { get; set; }
        public Guid? ParentModuleId { get; set; }

        public ModuleEntity? ParentModule { get; set; }
        public ICollection<ModuleEntity> ChildModules { get; set; } = [];
        public ICollection<PermissionEntity>? Permissions { get; set; } = [];
    }
}
