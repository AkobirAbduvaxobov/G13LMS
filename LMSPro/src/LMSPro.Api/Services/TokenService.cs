using LMSPro.Api.Configurations.Settings;
using LMSPro.Api.Dtos;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace LMSPro.Api.Services;

public class TokenService : ITokenService
{
    private readonly JwtSettings JwtSettings;

    public TokenService(JwtSettings jwtSettings)
    {
        JwtSettings = jwtSettings;
    }

    public string GetToken(UserGetDto userGetDto)
    {
        var IdentityClaims = new Claim[]
        {
            new Claim("UserId",userGetDto.UserId.ToString()),
            new Claim("FirstName",userGetDto.FirstName),
            new Claim("LastName",userGetDto.LastName),
            new Claim("UserName",userGetDto.UserName),
            new Claim(ClaimTypes.Role,userGetDto.Role.ToString()),
            new Claim(ClaimTypes.Email,userGetDto.Email)
        };

        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(JwtSettings.SecretKey));
        var keyCredentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

        var expiresMinutes = JwtSettings.Lifetime;
        var token = new JwtSecurityToken(
            issuer: JwtSettings.Issuer,
            audience: JwtSettings.Audience,
            claims: IdentityClaims,
            expires: DateTime.Now.AddMinutes(expiresMinutes),
            signingCredentials: keyCredentials
            );

        return new JwtSecurityTokenHandler().WriteToken(token);
    }
}
