using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolManagement.Domain.Entities.SecurityModule
{
    public class UserEntity : BaseEntity
    {
        public string Username { get; set; } = null!;
        public string Email { get; set; } = null!;
        public string Password { get; set; } = null!;
        public string FirstName { get; set; } = null!;
        public string? LastName { get; set; }
        public string MobileNumber { get; set; } = null!;
        public DateTime? LastLoginAt { get; set; }
        public bool IsLocked { get; set; }

        public ICollection<UserRoleEntity>? UserRoles { get; set; } = [];
    }
}
