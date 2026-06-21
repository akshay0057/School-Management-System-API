using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolManagement.Application.DTOs.Permission
{
    public sealed class PermissionMatrixItemResponse
    {
        public Guid PermissionId { get; set; }

        public string PermissionName { get; set; } = null!;

        public string PermissionCode { get; set; } = null!;

        public bool Assigned { get; set; }
    }
}
