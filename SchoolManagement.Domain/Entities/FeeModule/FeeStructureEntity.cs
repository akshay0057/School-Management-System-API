using SchoolManagement.Domain.Entities.AcademicModule;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolManagement.Domain.Entities.FeeModule
{
    public class FeeStructureEntity : BaseEntity
    {
        public Guid ClassId { get; set; }
        public decimal MonthlyFee { get; set; }
        public DateTime EffectiveFrom { get; set; }
        public DateTime? EffectiveTo { get; set; }

        public ClassEntity? Class { get; set; }
    }
}
