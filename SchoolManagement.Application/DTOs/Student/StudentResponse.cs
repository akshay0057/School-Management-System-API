using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolManagement.Application.DTOs.Student
{
    public class StudentResponse
    {
        public Guid Id { get; set; }
        public string AdmissionNo { get; set; } = null!;
        public string RollNo { get; set; } = null!;
        public string FullName { get; set; } = null!;
        public string ClassName { get; set; } = null!;
        public string SectionName { get; set; } = null!;
        public string ParentName { get; set; } = null!;
        public string ParentMobile { get; set; } = null!;
    }
}
