using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Identity.DTOs;
using Microsoft.EntityFrameworkCore;
using AutoMapper.QueryableExtensions;
using AutoMapper;
using System;
using Identity.DBAccess.Interfaces;
using Identity.DBAccess.Entities;
using Identity.DBAccess.Data;

namespace Identity.DBAccess.Data
{
    public class UserRepository : IUserRepository
    {
        private readonly DataContext context;
        private readonly IMapper mapper;

        public UserRepository(DataContext context, IMapper mapper)
        {
            this.context = context;
            this.mapper = mapper;
        }

        public async Task<AppUser> GetUserByEmailAsync(string email)
        {
            return await context.Users
            .SingleOrDefaultAsync(x=>x.NormalizedEmail==email.ToUpper());
        }

        public async Task<AppUser> GetUserByIdAsync(int id)
        {
            return await context.Users.FindAsync(id);
        }

        public async Task<AppUser> GetUserByUsernameAsync(string username)
        {
            return await context.Users
            .SingleOrDefaultAsync(x=>x.UserName==username);
        }

        public async Task<IEnumerable<AppUser>> GetUsersAsync(GetUsersFilter filter)
        {
            var query = context.Users.AsQueryable();

            if (!string.IsNullOrEmpty(filter.EmailLike))
            {
                query = query.Where(x => x.Email.ToLower().Contains(filter.EmailLike))
                    .OrderBy(x => x.Email.Length)
                    .AsQueryable();
            }
            if (filter.UserIds.Any())
            {
                query = query.Where(x => filter.UserIds.Contains(x.Id))
                    .AsQueryable();
            }
            if (filter.EmailConfirmed)
            {
                query = query.Where(x => filter.EmailConfirmed == true)
                    .AsQueryable();
            }
            if (filter.UsersLimit > 0)
            {
                query = query
                    .Take(filter.UsersLimit)
                    .AsQueryable();
            }

            return await query.ToListAsync();
        }

        public void Update(AppUser user)
        {
            context.Entry(user).State=EntityState.Modified;
        }
    }
}