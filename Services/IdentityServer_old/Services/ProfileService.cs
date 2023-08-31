using Identity;
using IdentityModel;
using IdentityServer4.Extensions;
using IdentityServer4.Models;
using IdentityServer4.Services;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace IdentityServer.Services
{
    public class ProfileService : IProfileService
    {

        private readonly IUserClaimsPrincipalFactory<AppUser> _userClaimsPrincipalFactory;
        private readonly UserManager<AppUser> _userMgr;
        private readonly RoleManager<IdentityRole<int>> _roleMgr;

        public ProfileService(
            UserManager<AppUser> userMgr,
            RoleManager<IdentityRole<int>> roleMgr,
            IUserClaimsPrincipalFactory<AppUser> userClaimsPrincipalFactory)
        {
            _userMgr = userMgr;
            _roleMgr = roleMgr;
            _userClaimsPrincipalFactory = userClaimsPrincipalFactory;
        }

        public async Task GetProfileDataAsync(ProfileDataRequestContext context)
        {
            string sub = context.Subject.GetSubjectId();
            AppUser user = await _userMgr.FindByIdAsync(sub);
            ClaimsPrincipal userClaims = await _userClaimsPrincipalFactory.CreateAsync(user);


            List<Claim> claims = userClaims.Claims.ToList();
            claims = claims.Where(claim => context.RequestedClaimTypes.Contains(claim.Type)).ToList();
            claims.Add(new Claim(JwtClaimTypes.Email, user.Email));
            //claims.Add(new Claim(JwtClaimTypes.GivenName, user.FirstName));
            //if (_userMgr.SupportsUserRole)
            //{
            //    IList<string> roles = await _userMgr.GetRolesAsync(user);
            //    foreach(var rolename in roles)
            //    {
            //        claims.Add(new Claim(JwtClaimTypes.Role, rolename));
            //        if (_roleMgr.SupportsRoleClaims)
            //        {
            //            IdentityRole role = await _roleMgr.FindByNameAsync(rolename);
            //            if (role != null)
            //            {
            //                claims.AddRange(await _roleMgr.GetClaimsAsync(role));
            //            }
            //        }
            //    }
            //}

            context.IssuedClaims = claims;
        }

        public async Task IsActiveAsync(IsActiveContext context)
        {
            string sub = context.Subject.GetSubjectId();
            AppUser user = await _userMgr.FindByIdAsync(sub);
            context.IsActive = user != null;
        }
    }
}
