using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolManagement.Application.DTOs.Student
{
    public class UpdateStudentRequest : AddStudentRequest
    {
        public Guid Id { get; set; }
    }
}
