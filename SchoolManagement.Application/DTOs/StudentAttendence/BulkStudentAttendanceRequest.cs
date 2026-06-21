using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolManagement.Application.DTOs.StudentAttendence
{
    public class BulkStudentAttendanceRequest
    {
        public DateTime AttendanceDate { get; set; }

        public List<StudentAttendanceItem> Students { get; set; } = [];
    }

    public class StudentAttendanceItem
    {
        public Guid StudentId { get; set; }

        public string Status { get; set; } = null!;
    }
}
