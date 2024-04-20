using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using APICatalogo.Communication.DTOs;
using APICatalogo.Domain.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace APICatalogo.Application.Services.Token;

public class TokenService : ITokenService
{

    private readonly UserManager<ApplicationUser> _userManager;
    private readonly RoleManager<IdentityRole> _roleManager;
    private readonly IConfiguration _config;

    public TokenService(
        UserManager<ApplicationUser> userManager,
        RoleManager<IdentityRole> roleManager,
        IConfiguration configuration
    )
    {
        _userManager = userManager;
        _roleManager = roleManager;
        _config = configuration;
    }

    public async Task<string> GenerateAccessToken(ApplicationUser user) {

        var key = _config["JWT:SecretKey"]
            ?? throw new InvalidOperationException("Invalid secret key.");

        var privateKey = Encoding.UTF8.GetBytes(key);

        var signingCredentials = new SigningCredentials(
            new SymmetricSecurityKey(privateKey),
            SecurityAlgorithms.HmacSha256Signature
        );

        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(await GetAllValidClaims(user)),
            Expires = DateTime.UtcNow.AddMinutes(
                _config.GetSection("JWT")
                    .GetValue<double>("TokenValidityInMinutes")
            ),
            Audience = _config.GetSection("JWT:ValidAudience").Value,
            Issuer = _config.GetSection("JWT:ValidIssuer").Value,
            SigningCredentials = signingCredentials
        };

        var tokenHandler = new JwtSecurityTokenHandler();
        var createToken = tokenHandler.CreateJwtSecurityToken(tokenDescriptor);
        var token = tokenHandler.WriteToken(createToken);

        return token;

    }


    private async Task<List<Claim>> GetAllValidClaims(ApplicationUser user)
    {

        var claims = new List<Claim> {
            new Claim("Id", user.Id.ToString()),
            new Claim(JwtRegisteredClaimNames.Sub, value: user.Email),
            new Claim(JwtRegisteredClaimNames.Email, value: user.Email),
            new Claim(JwtRegisteredClaimNames.Jti, value: Guid.NewGuid().ToString()),
            new Claim(JwtRegisteredClaimNames.Iat, value: DateTime.Now.ToUniversalTime().ToString()),
        };

        var userClaims = await _userManager.GetClaimsAsync(user);
        claims.AddRange(userClaims);

        var userRoles = await _userManager.GetRolesAsync(user);

        foreach (var userRole in userRoles)
        {

            var role = await _roleManager.FindByNameAsync(userRole);

            if (role != null)
            {

                claims.Add(new Claim(ClaimTypes.Role, userRole));

                var roleClaims = await _roleManager.GetClaimsAsync(role);

                foreach (var roleClaim in roleClaims)
                {
                    claims.Add(roleClaim);
                }

            }
        }

        return claims;

    }

}
