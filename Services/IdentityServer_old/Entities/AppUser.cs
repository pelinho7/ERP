using Microsoft.AspNetCore.Identity;

namespace Identity
{
    public class AppUser : IdentityUser<int>
    {
        public int? CompanyId { get; set; }
        public int? CreatorId { get; set; }
        public int CompanyType { get; set; } = 0;
    }
}
