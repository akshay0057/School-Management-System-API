using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolManagement.Application.DTOs.Staff
{
    public class AddStaffRequest
    {
        public string EmployeeCode { get; set; } = null!;

        public string FirstName { get; set; } = null!;

        public string? LastName { get; set; }

        public string Gender { get; set; } = null!;

        public string Mobile { get; set; } = null!;

        public string Email { get; set; } = null!;

        public string Designation { get; set; } = null!;

        public DateTime JoiningDate { get; set; }

        public decimal BasicSalary { get; set; }
    }
}
