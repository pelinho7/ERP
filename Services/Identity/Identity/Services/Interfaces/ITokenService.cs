using System.Threading.Tasks;
using Identity.DBAccess.Entities;

namespace Identity.Services.Interfaces
{
    public interface ITokenService
    {
        Task<string> CreateToken(AppUser user);
    }
}