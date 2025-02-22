using Microsoft.AspNetCore.Identity;

namespace Identity.DBAccess.Entities
{
    public class AppRole : IdentityRole<int>
    {
        public ICollection<AppUserRole> UserRoles { get; set; }
    }

}
