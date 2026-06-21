using SchoolManagement.Domain.Entities.AcademicModule;
using SchoolManagement.Infrastructure.Persistence.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolManagement.Infrastructure.Persistence.Repositories.Implementations
{
    public class SectionRepository : GenericRepository<SectionEntity>, ISectionRepository
    {
        public SectionRepository(ApplicationDbContext context) : base(context)
        {
        }
    }
}
