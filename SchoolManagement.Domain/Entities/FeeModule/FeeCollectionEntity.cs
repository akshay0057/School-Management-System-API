using SchoolManagement.Domain.Entities.StudentModule;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolManagement.Domain.Entities.FeeModule
{
    public class FeeCollectionEntity : BaseEntity
    {
        public Guid StudentId { get; set; }
        public int Month { get; set; }
        public int Year { get; set; }
        public decimal Amount { get; set; }
        public DateTime PaymentDate { get; set; }
        public string PaymentMode { get; set; } = null!;
        public string ReceiptNo { get; set; } = null!;

        public StudentEntity? Student { get; set; }
    }
}
