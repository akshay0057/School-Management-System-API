using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolManagement.Application.DTOs.FeeCollection
{
    public class StudentFeeHistoryResponse
    {
        public Guid StudentId { get; set; }

        public string StudentName { get; set; } = string.Empty;

        public List<StudentFeeHistoryItem> FeeHistory { get; set; } = [];
    }

    public class StudentFeeHistoryItem
    {
        public int Month { get; set; }

        public int Year { get; set; }

        public decimal Amount { get; set; }

        public DateTime PaymentDate { get; set; }

        public string ReceiptNo { get; set; } = string.Empty;
    }
}
