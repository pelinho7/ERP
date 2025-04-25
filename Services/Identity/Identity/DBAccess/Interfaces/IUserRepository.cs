using System.Collections.Generic;
using System.Threading.Tasks;
using Identity.DBAccess.Entities;
using Identity.DTOs;

namespace Identity.DBAccess.Interfaces
{
    public interface IUserRepository
    {
        void Update(AppUser user);
        Task<IEnumerable<AppUser>> GetUsersAsync(GetUsersFilter filter);
        Task<AppUser> GetUserByIdAsync(int id);
        Task<AppUser> GetUserByUsernameAsync(string username);
        Task<AppUser> GetUserByEmailAsync(string email);
    }
}