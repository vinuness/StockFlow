using Estoque.Domain.Entities.Clientes;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;

namespace Estoque.Infrastructure.Utilidades
{
    public class JWTService
    {
        private readonly string _key;

        public JWTService(IConfiguration configuration)
        {
            _key = configuration["jwt:key"];
        }

        public string GenerateToken(Cliente cliente) //gera o token JWT para o cliente, contendo ID, nome, email e roles do cliente como claims
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_key));
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.NameIdentifier, cliente.Id.ToString()),
                    new Claim(ClaimTypes.Name, cliente.Nome),
                    new Claim(ClaimTypes.Email, cliente.Email),
                    new Claim(ClaimTypes.Role, cliente.Roles)
                }),
                Expires = DateTime.UtcNow.AddHours(1),
                SigningCredentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256Signature)
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);

            return tokenHandler.WriteToken(token);
        }
    }
}
