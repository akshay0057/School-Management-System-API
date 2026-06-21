using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SchoolManagement.Domain.Entities.AttendanceModule;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolManagement.Infrastructure.Persistence.Configurations
{
    public class StaffAttendanceConfiguration : IEntityTypeConfiguration<StaffAttendanceEntity>
    {
        public void Configure(EntityTypeBuilder<StaffAttendanceEntity> builder)
        {
            builder.ToTable("StaffAttendances");

            builder.HasKey(x => x.Id);

            builder.Property(x => x.AttendanceDate)
                   .IsRequired();

            builder.Property(x => x.Status)
                   .IsRequired()
                   .HasMaxLength(20);

            // One attendance per staff per day
            builder.HasIndex(x => new
            {
                x.StaffId,
                x.AttendanceDate
            }).IsUnique();

            builder.HasOne(x => x.Staff)
                   .WithMany()
                   .HasForeignKey(x => x.StaffId)
                   .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
