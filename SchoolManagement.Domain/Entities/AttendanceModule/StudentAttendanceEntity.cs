using SchoolManagement.Domain.Entities.StudentModule;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolManagement.Domain.Entities.AttendanceModule
{
    public class StudentAttendanceEntity : BaseEntity
    {
        public Guid StudentId { get; set; }
        public DateTime AttendanceDate { get; set; }
        public string Status { get; set; } = null!;

        public StudentEntity? Student { get; set; }
    }
}
