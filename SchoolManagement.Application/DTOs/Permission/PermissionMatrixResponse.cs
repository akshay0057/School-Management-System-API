using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolManagement.Application.DTOs.Permission
{
    public sealed class PermissionMatrixResponse
    {
        public Guid RoleId { get; set; }

        public string RoleName { get; set; } = null!;

        public List<PermissionMatrixItemResponse> Permissions { get; set; } = [];
    }
}
