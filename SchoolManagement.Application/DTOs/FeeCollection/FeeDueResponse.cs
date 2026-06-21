using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolManagement.Application.DTOs.FeeCollection
{
    public class FeeDueResponse
    {
        public Guid StudentId { get; set; }

        public string StudentName { get; set; } = string.Empty;

        public decimal MonthlyFee { get; set; }

        public int PendingMonths { get; set; }

        public decimal TotalDue { get; set; }
    }
}
