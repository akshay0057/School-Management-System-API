using SchoolManagement.Domain.Entities.FeeModule;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolManagement.Infrastructure.Persistence.Repositories.Interfaces
{
    public interface IFeeCollectionRepository
    : IGenericRepository<FeeCollectionEntity>
    {
        Task<bool> FeeAlreadyCollectedAsync(
            Guid studentId,
            int month,
            int year);

        Task<List<FeeCollectionEntity>>GetByStudentIdAsync(Guid studentId);
    }
}
