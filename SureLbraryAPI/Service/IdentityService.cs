using Microsoft.IdentityModel.Tokens;
using SureLbraryAPI.DTOs;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace SureLbraryAPI.Service
{
    public class IdentityService(IConfiguration configuration)
    {
        public string CreateToken(CreateUserDTO createUser)
        {
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier,createUser.Name),
            };
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration.GetValue<string>("JwtSettings:SigningKey")!));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha512);
            var TokenDescriptor = new JwtSecurityToken(

                issuer: configuration.GetValue<string>("JwtSettings:Issuer"),
                claims: claims,
                expires: DateTime.UtcNow.AddHours(1),
                signingCredentials: creds,
                audience: configuration.GetValue<string>("JwtSettings:Audience")
            );
            return new JwtSecurityTokenHandler().WriteToken(TokenDescriptor);
        }
    }
}
