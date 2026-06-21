using SchoolManagement.Domain.Entities.FeeModule;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolManagement.Infrastructure.Persistence.Repositories.Interfaces
{
    public interface IFeeStructureRepository
    : IGenericRepository<FeeStructureEntity>
    {
        Task<FeeStructureEntity?>GetByClassIdAsync(Guid classId);
    }
}
