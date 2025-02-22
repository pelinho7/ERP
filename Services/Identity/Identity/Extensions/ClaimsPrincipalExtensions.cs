using System.Security.Claims;

namespace Identity.Extensions
{
    public static class ClaimsPrincipalExtensions
    {
        public static string GetUserName(this ClaimsPrincipal user){
            return user.FindFirst(ClaimTypes.Name)?.Value;
        }

        public static int GetUserId(this ClaimsPrincipal user){
            return int.Parse(user.FindFirst(ClaimTypes.NameIdentifier)?.Value);
        }

        public static string GetUserEmail(this ClaimsPrincipal user){
            return  user.FindFirst(ClaimTypes.Email)?.Value;
        }
    }
}