using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolManagement.Application.DTOs.Staff
{
    public class StaffResponse
    {
        public Guid Id { get; set; }

        public string EmployeeCode { get; set; } = null!;

        public string FullName { get; set; } = null!;

        public string Mobile { get; set; } = null!;

        public string Email { get; set; } = null!;

        public string Designation { get; set; } = null!;

        public decimal BasicSalary { get; set; }

        public DateTime JoiningDate { get; set; }
    }
}
