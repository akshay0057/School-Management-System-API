using Microsoft.EntityFrameworkCore;
using SchoolManagement.Domain;
using SchoolManagement.Domain.Entities.AcademicModule;
using SchoolManagement.Domain.Entities.AttendanceModule;
using SchoolManagement.Domain.Entities.FeeModule;
using SchoolManagement.Domain.Entities.SecurityModule;
using SchoolManagement.Domain.Entities.StaffModule;
using SchoolManagement.Domain.Entities.StudentModule;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolManagement.Infrastructure.Persistence
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(
            DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        #region Security Module

        public DbSet<UserEntity> Users { get; set; }

        public DbSet<RoleEntity> Roles { get; set; }

        public DbSet<PermissionEntity> Permissions { get; set; }

        public DbSet<ModuleEntity> Modules { get; set; }

        public DbSet<UserRoleEntity> UserRoles { get; set; }

        public DbSet<RolePermissionEntity> RolePermissions { get; set; }

        #endregion

        #region Academic Module

        public DbSet<AcademicSessionEntity> AcademicSessions { get; set; }

        public DbSet<ClassEntity> Classes { get; set; }

        public DbSet<SectionEntity> Sections { get; set; }

        #endregion

        #region Academics

        public DbSet<StudentEntity> Students { get; set; }

        public DbSet<StaffEntity> Staffs { get; set; }

        #endregion

        #region Attendance

        public DbSet<StudentAttendanceEntity> StudentAttendances { get; set; }

        public DbSet<StaffAttendanceEntity> StaffAttendances { get; set; }

        #endregion

        #region Fees

        public DbSet<FeeStructureEntity> FeeStructures { get; set; }

        public DbSet<FeeCollectionEntity> FeeCollections { get; set; }

        #endregion

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Apply all IEntityTypeConfiguration<T>
            modelBuilder.ApplyConfigurationsFromAssembly(
                typeof(ApplicationDbContext).Assembly);
        }

        public override int SaveChanges()
        {
            ApplyAuditInformation();
            return base.SaveChanges();
        }

        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            ApplyAuditInformation();
            return base.SaveChangesAsync(cancellationToken);
        }

        private void ApplyAuditInformation()
        {
            foreach (var entry in ChangeTracker.Entries<BaseEntity>())
            {
                if (entry.State == EntityState.Added)
                {
                    if (entry.Entity.Id == Guid.Empty)
                        entry.Entity.Id = Guid.NewGuid();

                    entry.Entity.CreatedAt = DateTime.UtcNow;
                }

                if (entry.State == EntityState.Modified)
                {
                    entry.Entity.UpdatedAt = DateTime.UtcNow;
                }
            }
        }
    }
}
