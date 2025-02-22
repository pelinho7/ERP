using Dictionaries.Enums;
using Identity;
using Identity.DBAccess.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Identity.DBAccess.Data
{
    public class UserSeed
    {
        public static async Task SeedAsync(UserManager<AppUser> userManager
            , RoleManager<AppRole> roleManager, ILogger<UserSeed> logger)
        {
            if (userManager.Users.Any()) return;

            if (await userManager.Users.AnyAsync()) return;

            // var userData = await System.IO.File.ReadAllTextAsync("Data/UserSeedData.json");
            // var users = JsonSerializer.Deserialize<List<AppUser>>(userData);
            // if (users == null) return;

            var roles = new List<AppRole>
            {
                new AppRole{Name = AppRoleEnum.SysAdmin.ToString()},
                new AppRole{Name = AppRoleEnum.CompanyAdmin.ToString()},
                new AppRole{Name = AppRoleEnum.User.ToString()},
            };

            foreach (var role in roles)
            {
                await roleManager.CreateAsync(role);
            }

            // foreach (var user in users)
            // {
            //     user.UserName = user.UserName.ToLower();
            //     await userManager.CreateAsync(user,"Pa$$w0rd");
            //     await userManager.AddToRoleAsync(user, "Member");
            // }
            var admin = new AppUser
            {
                UserName = "admin",
                Email = "patryk.pele@op.pl",
                EmailConfirmed=true,
            };

            await userManager.CreateAsync(admin, "Pa$$w0rd");
            //await userManager.AddToRolesAsync(admin, new[] { AppRoleEnum.Admin.ToString(), AppRoleEnum.User.ToString() });






            //var admin = new AppUser
            //{
            //    UserName = "admin"
            //};

            //await userManager.CreateAsync(admin, "Pa$$w0rd");
        }

        //private static IEnumerable<Order> GetPreconfiguredOrders()
        //{
        //    return new List<Order>
        //    {
        //        new Order() {UserName = "swn", FirstName = "Mehmet", LastName = "Ozkaya", EmailAddress = "ezozkme@gmail.com", AddressLine = "Bahcelievler", Country = "Turkey", TotalPrice = 350 }
        //    };
        //}
    }
}
