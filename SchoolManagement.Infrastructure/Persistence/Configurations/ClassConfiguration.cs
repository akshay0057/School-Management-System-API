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
    public class ClassConfiguration : IEntityTypeConfiguration<ClassEntity>
    {
        public void Configure(EntityTypeBuilder<ClassEntity> builder)
        {
            builder.ToTable("Classes");

            builder.HasKey(x => x.Id);

            builder.Property(x => x.ClassName)
                   .IsRequired()
                   .HasMaxLength(50);

            builder.Property(x => x.DisplayOrder)
                   .IsRequired();

            builder.HasIndex(x => x.ClassName)
                   .IsUnique();
        }
    }
}
