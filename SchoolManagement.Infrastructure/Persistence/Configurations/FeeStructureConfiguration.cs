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
    public class FeeStructureConfiguration : IEntityTypeConfiguration<FeeStructureEntity>
    {
        public void Configure(EntityTypeBuilder<FeeStructureEntity> builder)
        {
            builder.ToTable("FeeStructures");

            builder.HasKey(x => x.Id);

            builder.Property(x => x.MonthlyFee)
                   .HasColumnType("decimal(18,2)")
                   .IsRequired();

            builder.Property(x => x.EffectiveFrom)
                   .IsRequired();

            builder.HasOne(x => x.Class)
                   .WithMany()
                   .HasForeignKey(x => x.ClassId)
                   .OnDelete(DeleteBehavior.Restrict);

            builder.HasIndex(x => new
            {
                x.ClassId,
                x.EffectiveFrom
            });
        }
    }
}
