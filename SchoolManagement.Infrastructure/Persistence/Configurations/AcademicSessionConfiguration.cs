using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SchoolManagement.Domain.Entities.AcademicModule;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolManagement.Infrastructure.Persistence.Configurations
{
    public class AcademicSessionConfiguration : IEntityTypeConfiguration<AcademicSessionEntity>
    {
        public void Configure(EntityTypeBuilder<AcademicSessionEntity> builder)
        {
            builder.ToTable("AcademicSessions");

            builder.HasKey(x => x.Id);

            builder.Property(x => x.SessionName)
                   .IsRequired()
                   .HasMaxLength(50);

            builder.Property(x => x.StartDate)
                   .IsRequired();

            builder.Property(x => x.EndDate)
                   .IsRequired();

            builder.Property(x => x.IsCurrent)
                   .IsRequired();

            builder.HasIndex(x => x.SessionName)
                   .IsUnique();
        }
    }
}
