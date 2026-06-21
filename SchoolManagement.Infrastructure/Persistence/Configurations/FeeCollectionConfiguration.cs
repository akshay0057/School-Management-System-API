using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SchoolManagement.Domain.Entities.FeeModule;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolManagement.Infrastructure.Persistence.Configurations
{
    public class FeeCollectionConfiguration : IEntityTypeConfiguration<FeeCollectionEntity>
    {
        public void Configure(EntityTypeBuilder<FeeCollectionEntity> builder)
        {
            builder.ToTable("FeeCollections");

            builder.HasKey(x => x.Id);

            builder.Property(x => x.Amount)
                   .HasColumnType("decimal(18,2)")
                   .IsRequired();

            builder.Property(x => x.PaymentDate)
                   .IsRequired();

            builder.Property(x => x.PaymentMode)
                   .IsRequired()
                   .HasMaxLength(50);

            builder.Property(x => x.ReceiptNo)
                   .IsRequired()
                   .HasMaxLength(50);

            builder.HasIndex(x => x.ReceiptNo)
                   .IsUnique();

            builder.HasOne(x => x.Student)
                   .WithMany()
                   .HasForeignKey(x => x.StudentId)
                   .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
