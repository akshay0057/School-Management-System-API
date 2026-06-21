using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolManagement.Application.DTOs.RolePermission
{
    public sealed class AssignRolePermissionRequest
    {
        public Guid RoleId { get; set; }

        public List<Guid> PermissionIds { get; set; } = [];
    }
}
