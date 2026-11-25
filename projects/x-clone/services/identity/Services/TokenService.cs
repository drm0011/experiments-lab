using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Identity.Service.Models;
using Microsoft.IdentityModel.Tokens;

namespace Identity.Service.Services;

public class TokenService(IConfiguration configuration)
{
    public string Issue(User user)
    {
        string key = configuration["Jwt:Key"]
            ?? throw new InvalidOperationException("Jwt:Key is not configured.");
        string issuer = configuration["Jwt:Issuer"] ?? "x-clone";
        string audience = configuration["Jwt:Audience"] ?? "x-clone";

        Claim[] claims =
        [
            new(JwtRegisteredClaimNames.Sub, user.Id.ToString()),
            new(JwtRegisteredClaimNames.UniqueName, user.Username),
            new(JwtRegisteredClaimNames.Email, user.Email)
        ];

        SymmetricSecurityKey signingKey = new(Encoding.UTF8.GetBytes(key));
        SigningCredentials credentials = new(signingKey, SecurityAlgorithms.HmacSha256);

        JwtSecurityToken token = new(
            issuer: issuer,
            audience: audience,
            claims: claims,
            expires: DateTime.UtcNow.AddHours(1),
            signingCredentials: credentials);

        return new JwtSecurityTokenHandler().WriteToken(token);
    }
}
