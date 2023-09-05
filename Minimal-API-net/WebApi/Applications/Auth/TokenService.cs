using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using WebApi.Domain.Auth.Interfaces.Service;
using WebApi.Domain.Auth.Models;
using WebApi.Infra.Data;

namespace WebApi.Applications.Auth
{
    public class TokenService: ITokenService
    {
        public string Generate(Users user)
        {
            //Implementar quando ao tentar criar um usario sem espeficiar um Role , colocar uma role defaout

            var handler = new JwtSecurityTokenHandler();

            var key = Encoding.ASCII.GetBytes(JwtConfiguration.Key);

            var crerdentials = new SigningCredentials(
                new SymmetricSecurityKey(key),
                algorithm: SecurityAlgorithms.HmacSha256Signature);

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = GenerateClaim(user),
                SigningCredentials = crerdentials,
                Expires = DateTime.UtcNow.AddMinutes(1),
            };

            var token = handler.CreateToken(tokenDescriptor);
            
            return handler.WriteToken(token);
        }
        private static ClaimsIdentity GenerateClaim(Users user)
        {
            var claims = new List<Claim>();

            claims.Add(new Claim(type: ClaimTypes.Name, value: user.Username));

            if(user.Roles != null)
            {
                claims.AddRange(user.Roles.Select(role => new Claim(ClaimTypes.Role, role)));
            }

            var claimsIdentity = new ClaimsIdentity(claims, "custom", ClaimTypes.Name, ClaimTypes.Role);

            return claimsIdentity;
        }

    }
}
