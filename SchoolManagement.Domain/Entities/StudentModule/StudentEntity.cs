using SchoolManagement.Domain.Entities.AcademicModule;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolManagement.Domain.Entities.StudentModule
{
    public class StudentEntity : BaseEntity
    {
        public string AdmissionNo { get; set; } = null!;
        public string RollNo { get; set; } = null!;
        public string FirstName { get; set; } = null!;
        public string? LastName { get; set; }
        public string? Gender { get; set; }
        public DateTime DateOfBirth { get; set; }
        public DateTime AdmissionDate { get; set; }
        public Guid ClassId { get; set; }
        public Guid SectionId { get; set; }
        public Guid SessionId { get; set; }
        public string? ParentName { get; set; }
        public string? ParentMobile { get; set; }
        public string? Address { get; set; }

        public ClassEntity? Class { get; set; }
        public SectionEntity? Section { get; set; }
        public AcademicSessionEntity? Session { get; set; }
    }
}
