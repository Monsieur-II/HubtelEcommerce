using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Commerce.Infras.Models;
using Microsoft.IdentityModel.Tokens;

namespace Commerce.Application.Services.Auth;

public static class TokenService
{
    public static async Task<string> GenerateTokenAsync(AppUser user, JWTConfig config)
    {
        var userClaims = new List<Claim>
        {
            new(ClaimTypes.Name, user.UserName),
            new(ClaimTypes.NameIdentifier, user.Id)
        };

        var authSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(config.TokenKey));
        var token = new JwtSecurityToken(
            issuer: config.ValidIssuer,
            audience: config.ValidAudience,
            expires: DateTime.Now.AddHours(config.TokenExpiry),
            claims: userClaims,
            signingCredentials: new SigningCredentials(authSigningKey, SecurityAlgorithms.HmacSha256)
        );
        
        return new JwtSecurityTokenHandler().WriteToken(token);
    }
}
