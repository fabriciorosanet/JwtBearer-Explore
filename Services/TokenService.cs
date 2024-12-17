using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using JwtBearer.Models;
using Microsoft.AspNetCore.Components;
using Microsoft.IdentityModel.Tokens;

namespace JwtBearer.Services;

public class TokenService
{
    public string GenerateToken(User user)
    {
        // Cria uma instância do JwtSecurityTokenHandler
        var handler = new JwtSecurityTokenHandler();

        var key = Encoding.ASCII.GetBytes(Configuration.PrivateKey);
        
        var credentials = new SigningCredentials(new SymmetricSecurityKey(key),
            SecurityAlgorithms.HmacSha256Signature);

        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = GenerateClaimsIdentity(user),
            SigningCredentials = credentials,
            Expires = DateTime.UtcNow.AddHours(1),
        };
        
        // Gera um Token
        var token = handler.CreateToken(tokenDescriptor);
        
        // Gera uma string do Token
        return handler.WriteToken(token);
        
    }

    private static ClaimsIdentity GenerateClaimsIdentity(User user)
    {
        var ci = new ClaimsIdentity();
        ci.AddClaim(new Claim(ClaimTypes.Name, user.Email));
        return ci;
    }
}