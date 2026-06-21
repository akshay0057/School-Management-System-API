using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SchoolManagement.Domain.Entities.StudentModule;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolManagement.Infrastructure.Persistence.Configurations
{
    public class StudentConfiguration : IEntityTypeConfiguration<StudentEntity>
    {
        public void Configure(EntityTypeBuilder<StudentEntity> builder)
        {
            builder.ToTable("Students");

            builder.HasKey(x => x.Id);

            builder.Property(x => x.AdmissionNo)
                   .IsRequired()
                   .HasMaxLength(50);

            builder.Property(x => x.RollNo)
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

            builder.Property(x => x.ParentName)
                   .IsRequired()
                   .HasMaxLength(150);

            builder.Property(x => x.ParentMobile)
                   .IsRequired()
                   .HasMaxLength(15);

            builder.Property(x => x.Address)
                   .HasMaxLength(500);

            // Unique Admission Number
            builder.HasIndex(x => x.AdmissionNo)
                   .IsUnique();

            // Roll No unique within a Session + Class + Section
            builder.HasIndex(x => new
            {
                x.RollNo,
                x.ClassId,
                x.SectionId,
                x.SessionId
            }).IsUnique();

            builder.HasOne(x => x.Class)
                   .WithMany()
                   .HasForeignKey(x => x.ClassId)
                   .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(x => x.Section)
                   .WithMany()
                   .HasForeignKey(x => x.SectionId)
                   .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(x => x.Session)
                   .WithMany()
                   .HasForeignKey(x => x.SessionId)
                   .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
