using Microsoft.EntityFrameworkCore;
using SchoolManagement.Domain.Entities.FeeModule;
using SchoolManagement.Infrastructure.Persistence.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolManagement.Infrastructure.Persistence.Repositories.Implementations
{
    public class FeeCollectionRepository
    : GenericRepository<FeeCollectionEntity>,
      IFeeCollectionRepository
    {
        public FeeCollectionRepository(
            ApplicationDbContext context)
            : base(context)
        {
        }

        public async Task<bool> FeeAlreadyCollectedAsync(
            Guid studentId,
            int month,
            int year)
        {
            return await _context.FeeCollections
                .AnyAsync(x =>
                    x.StudentId == studentId
                    && x.Month == month
                    && x.Year == year);
        }

        public async Task<List<FeeCollectionEntity>>GetByStudentIdAsync(Guid studentId)
        {
            return await _context.FeeCollections
                .Where(x => x.StudentId == studentId)
                .OrderByDescending(x => x.Year)
                .ThenByDescending(x => x.Month)
                .ToListAsync();
        }
    }
}
