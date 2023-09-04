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
            if(user.Roles == null) {

                return "Adicione uma Roles";
            }

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
            var claimsIdentity = new ClaimsIdentity();

            claimsIdentity.AddClaim(
                new Claim(type: ClaimTypes.Name, value: user.Username)
                );

            foreach (string role in user.Roles)
            {

                claimsIdentity.AddClaim(
                    new Claim(
                        type: ClaimTypes.Role,
                        value: role)
                    );

                claimsIdentity.AddClaim(new Claim(type: "SISTEN", "D&D"));
            }

            return claimsIdentity;
        }

    }
}
