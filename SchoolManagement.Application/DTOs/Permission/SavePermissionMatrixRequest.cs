using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolManagement.Application.DTOs.Permission
{
    public sealed class SavePermissionMatrixRequest
    {
        public Guid RoleId { get; set; }

        public List<Guid> PermissionIds { get; set; } = [];
    }
}
