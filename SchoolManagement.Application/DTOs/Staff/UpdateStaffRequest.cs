using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolManagement.Application.DTOs.Staff
{
    public class UpdateStaffRequest : AddStaffRequest
    {
        public Guid Id { get; set; }
    }
}
