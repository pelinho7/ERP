using Microsoft.AspNetCore.Identity;

namespace Identity.DBAccess.Entities
{
    public class AppUser : IdentityUser<int>
    {
        public ICollection<AppUserRole> UserRoles { get; set; }
    }

}
