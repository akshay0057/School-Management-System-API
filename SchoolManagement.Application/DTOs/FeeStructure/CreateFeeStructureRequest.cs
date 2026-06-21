using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolManagement.Application.DTOs.FeeStructure
{
    public class CreateFeeStructureRequest
    {
        public Guid ClassId { get; set; }

        public decimal MonthlyFee { get; set; }

        public DateTime EffectiveFrom { get; set; }

        public DateTime? EffectiveTo { get; set; }
    }
}
