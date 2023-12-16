using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using talabat.core.Entites.identity;
using talabat.core.services;

namespace talabat.service
{
    public class TokenService : ITokenService
    {
        private readonly IConfiguration configuration;

        public TokenService(IConfiguration configuration)
        {
            this.configuration = configuration;
        }
        public async Task<string> CreateTokenAsync(AppUser user)
        {

            // PAYLOAD [Data] [Cliams]
            // 1. Private Cliam
            var AuthClaims = new List<Claim>()
            {
                new Claim (ClaimTypes.GivenName , user.DisplayName),
                new Claim (ClaimTypes.Email , user.Email)
            };
             
            // 2. Register Cliams

            var AuthKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["JWT:key"]));
            var Token = new JwtSecurityToken(
                            issuer : configuration["JWT:ValidIssuer"],
                            audience : configuration["JWT:ValidAudience"],
                            expires : DateTime.Now.AddDays(double.Parse(configuration["JWT:DurationInDays"])),
                            claims : AuthClaims ,
                            signingCredentials : new SigningCredentials(AuthKey , SecurityAlgorithms.HmacSha256)
                            );

            return new JwtSecurityTokenHandler().WriteToken(Token);
        }



    }
}
