using Microsoft.AspNetCore.Authorization;

namespace SchoolManagement.API.Authorization
{
    public class PermissionAttribute : AuthorizeAttribute
    {
        public PermissionAttribute(string permission)
        {
            Policy = permission;
        }
    }
}
