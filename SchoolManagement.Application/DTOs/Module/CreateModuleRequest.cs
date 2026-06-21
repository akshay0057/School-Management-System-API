using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolManagement.Application.DTOs.Module
{
    public sealed class CreateModuleRequest
    {
        public string Name { get; set; } = null!;

        public string? Code { get; set; }

        public string? Route { get; set; }

        public string? Icon { get; set; }

        public int DisplayOrder { get; set; }

        public Guid? ParentModuleId { get; set; }
    }
}
