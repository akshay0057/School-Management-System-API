using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolManagement.Domain.Entities.AcademicModule
{
    public class ClassEntity : BaseEntity
    {
        public string ClassName { get; set; } = null!;
        public int? DisplayOrder { get; set; }
    }
}
