using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SchoolManagement.Domain.Entities.SecurityModule;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolManagement.Infrastructure.Persistence.Configurations
{
    public class ModuleConfiguration : IEntityTypeConfiguration<ModuleEntity>
    {
        public void Configure(EntityTypeBuilder<ModuleEntity> builder)
        {
            builder.ToTable("Modules");

            builder.HasKey(x => x.Id);

            builder.Property(x => x.Name)
                   .IsRequired()
                   .HasMaxLength(100);

            builder.Property(x => x.Code)
                   .IsRequired()
                   .HasMaxLength(50);

            builder.Property(x => x.Route)
                   .HasMaxLength(200);

            builder.Property(x => x.Icon)
                   .HasMaxLength(100);

            builder.HasIndex(x => x.Code)
                   .IsUnique();

            builder.HasOne(x => x.ParentModule)
                   .WithMany(x => x.ChildModules)
                   .HasForeignKey(x => x.ParentModuleId)
                   .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
