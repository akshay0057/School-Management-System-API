using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolManagement.Application.DTOs.Master
{
    public class CreateClassRequest
    {
        public string ClassName { get; set; } = null!;

        public int? DisplayOrder { get; set; }
    }
}
