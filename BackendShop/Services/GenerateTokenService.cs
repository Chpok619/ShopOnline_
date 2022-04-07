using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Microsoft.IdentityModel.Tokens;
using Models;

namespace ShopBackend;

public class GenerateTokenService
{
    private readonly JWTKey _jwtKey;

    public GenerateTokenService(JWTKey jwtKey)
    {
        _jwtKey = jwtKey;
    }

    public string GenerateToken(AccountDTO accountDto)
    {
        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(new[]
            {
                new Claim(ClaimTypes.NameIdentifier, accountDto.Id.ToString())
            }),
            Expires = DateTime.UtcNow.Add(_jwtKey.LifeTime),
            Issuer = _jwtKey.Issuer,
            SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(_jwtKey.SigningKeyBytes),
                SecurityAlgorithms.HmacSha256Signature)
        };

        var tokenHandler = new JwtSecurityTokenHandler();
        var token = tokenHandler.CreateToken(tokenDescriptor);

        return tokenHandler.WriteToken(token);
    }
}