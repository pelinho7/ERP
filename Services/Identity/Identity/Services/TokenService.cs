 using System.Collections.Generic;
using System.Security.Claims;
using System.Text;
using Identity.DBAccess.Entities;
using Identity.Services.Interfaces;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System;
using Microsoft.AspNetCore.Identity;
using System.Threading.Tasks;
using System.Linq;
using Microsoft.Extensions.Options;
using Identity.Models;

namespace Identity.Services
{
    public class TokenService : ITokenService
    {
        private readonly SymmetricSecurityKey _key;
        private readonly UserManager<AppUser> userManager;
        private readonly JwtOptions _jwtOptions;


        public TokenService(IConfiguration config, UserManager<AppUser> userManager, IOptions<JwtOptions> jwtOptions)
        {
            _jwtOptions = jwtOptions.Value;
            _key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtOptions.Secret));
            //_key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(config["TokenKey"]));
            this.userManager = userManager;
        }

        public async Task<string> CreateToken(AppUser user)
        {
            var claims=new List<Claim>{
                new Claim(JwtRegisteredClaimNames.NameId,user.Id.ToString()),
                new Claim(JwtRegisteredClaimNames.UniqueName,user.UserName)
            };

            var roles=await userManager.GetRolesAsync(user);

            claims.AddRange(roles.Select(role=>new Claim(ClaimTypes.Role,role)));

            var creds=new SigningCredentials(_key,SecurityAlgorithms.HmacSha512Signature);

            var tokenDescriptor=new SecurityTokenDescriptor{
                Audience=_jwtOptions.Audience,
                Issuer = _jwtOptions.Issuer,
                Subject =new ClaimsIdentity(claims),
                Expires=DateTime.Now.AddDays(7),
                SigningCredentials=creds
            };

            var tokenHandler=new JwtSecurityTokenHandler();
            var token=tokenHandler.CreateToken(tokenDescriptor);

            return tokenHandler.WriteToken(token);
        }
    }
}