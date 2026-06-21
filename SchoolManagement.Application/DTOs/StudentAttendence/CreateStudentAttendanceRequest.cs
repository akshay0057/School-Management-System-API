using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolManagement.Application.DTOs.StudentAttendence
{
    public class CreateStudentAttendanceRequest
    {
        public Guid StudentId { get; set; }

        public DateTime AttendanceDate { get; set; }

        public string Status { get; set; } = null!;
    }
}
