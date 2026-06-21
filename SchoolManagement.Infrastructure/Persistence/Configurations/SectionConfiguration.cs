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
    public class SectionConfiguration : IEntityTypeConfiguration<SectionEntity>
    {
        public void Configure(EntityTypeBuilder<SectionEntity> builder)
        {
            builder.ToTable("Sections");

            builder.HasKey(x => x.Id);

            builder.Property(x => x.SectionName)
                   .IsRequired()
                   .HasMaxLength(20);

            builder.HasIndex(x => x.SectionName)
                   .IsUnique();
        }
    }
}
