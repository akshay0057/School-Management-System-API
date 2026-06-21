using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolManagement.Application.DTOs.Master
{
    public class CreateRoleRequest
    {
        public string RoleName { get; set; } = null!;
        public string? Description { get; set; }
    }
}
