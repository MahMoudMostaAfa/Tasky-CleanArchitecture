using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Tasky.Application.Common.Interfaces;


namespace Tasky.Infrastructure.Identity;

public class TokenProvider(IConfiguration _configuration) : ITokenProvider
{
  public string GenerateToken(UserDto user)
  {
    var jwtSettings = _configuration.GetSection("JwtSettings");
    var issuer = jwtSettings["Issuer"]!;
    var audience = jwtSettings["Audience"]!;
    var key = jwtSettings["Secret"]!;

    var expires = DateTime.UtcNow.AddMinutes(int.Parse(jwtSettings["TokenExpirationInMinutes"]!));

    List<Claim> claims = new()
    {
      new Claim (JwtRegisteredClaimNames.Sub , user.Id),
      new Claim (JwtRegisteredClaimNames.Email , user.Email),
    };

    foreach (var role in user.Roles!)
    {
      claims.Add(new Claim(ClaimTypes.Role, role));
    }

    var descriptor = new SecurityTokenDescriptor
    {
      Subject = new ClaimsIdentity(claims),
      Expires = expires,
      Issuer = issuer,
      Audience = audience,
      SigningCredentials = new SigningCredentials(
                new SymmetricSecurityKey(Encoding.UTF8.GetBytes(key)),
                SecurityAlgorithms.HmacSha256Signature),

    };

    var tokenHandler = new JwtSecurityTokenHandler();

    var securityToken = tokenHandler.CreateToken(descriptor);
    var token = tokenHandler.WriteToken(securityToken);

    return token;

  }
}