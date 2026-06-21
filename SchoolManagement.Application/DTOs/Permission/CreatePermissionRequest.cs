using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolManagement.Application.DTOs.Permission
{
    public sealed class CreatePermissionRequest
    {
        public Guid ModuleId { get; set; }

        public string Name { get; set; } = null!;

        public string Code { get; set; } = null!;
    }
}
