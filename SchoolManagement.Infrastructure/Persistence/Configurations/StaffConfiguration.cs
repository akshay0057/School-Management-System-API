using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SchoolManagement.Domain.Entities.StaffModule;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolManagement.Infrastructure.Persistence.Configurations
{
    public class StaffConfiguration : IEntityTypeConfiguration<StaffEntity>
    {
        public void Configure(EntityTypeBuilder<StaffEntity> builder)
        {
            builder.ToTable("Staffs");

            builder.HasKey(x => x.Id);

            builder.Property(x => x.EmployeeCode)
                   .IsRequired()
                   .HasMaxLength(20);

            builder.Property(x => x.FirstName)
                   .IsRequired()
                   .HasMaxLength(100);

            builder.Property(x => x.LastName)
                   .HasMaxLength(100);

            builder.Property(x => x.Gender)
                   .IsRequired()
                   .HasMaxLength(10);

            builder.Property(x => x.Mobile)
                   .IsRequired()
                   .HasMaxLength(15);

            builder.Property(x => x.Email)
                   .HasMaxLength(100);

            builder.Property(x => x.Designation)
                   .IsRequired()
                   .HasMaxLength(100);

            builder.Property(x => x.BasicSalary)
                   .HasColumnType("decimal(18,2)");

            builder.HasIndex(x => x.EmployeeCode)
                   .IsUnique();

            builder.HasIndex(x => x.Email)
                   .IsUnique();
        }
    }
}
