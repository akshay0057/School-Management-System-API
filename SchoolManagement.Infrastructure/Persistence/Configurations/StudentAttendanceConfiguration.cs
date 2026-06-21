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
    public class StudentAttendanceConfiguration : IEntityTypeConfiguration<StudentAttendanceEntity>
    {
        public void Configure(EntityTypeBuilder<StudentAttendanceEntity> builder)
        {
            builder.ToTable("StudentAttendances");

            builder.HasKey(x => x.Id);

            builder.Property(x => x.AttendanceDate)
                   .IsRequired();

            builder.Property(x => x.Status)
                   .IsRequired()
                   .HasMaxLength(20);

            // One attendance per student per day
            builder.HasIndex(x => new
            {
                x.StudentId,
                x.AttendanceDate
            }).IsUnique();

            builder.HasOne(x => x.Student)
                   .WithMany()
                   .HasForeignKey(x => x.StudentId)
                   .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
