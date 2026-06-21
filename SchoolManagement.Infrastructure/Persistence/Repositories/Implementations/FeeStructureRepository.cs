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
    public class FeeStructureRepository : GenericRepository<FeeStructureEntity>, IFeeStructureRepository
    {
        public FeeStructureRepository(
            ApplicationDbContext context)
            : base(context)
        {
        }

        public async Task<FeeStructureEntity?>GetByClassIdAsync(Guid classId)
        {
            return await _context.FeeStructures
                .FirstOrDefaultAsync(x =>
                    x.ClassId == classId);
        }
    }
}
