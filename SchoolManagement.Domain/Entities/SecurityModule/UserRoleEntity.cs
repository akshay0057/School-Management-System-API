using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolManagement.Domain.Entities.SecurityModule
{
    public class UserRoleEntity: BaseEntity
    {
        public Guid UserId { get; set; }
        public Guid RoleId { get; set; }

        public UserEntity? User { get; set; }
        public RoleEntity? Role { get; set; }
    }
}
