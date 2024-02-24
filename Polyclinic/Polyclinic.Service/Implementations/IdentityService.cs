using Microsoft.AspNetCore.Identity;
using Microsoft.Identity.Client;
using Microsoft.IdentityModel.Tokens;
using Polyclinic.Domain.Models;
using Polyclinic.Domain.Settings;
using Polyclinic.Service.Interfaces;
using System.Data;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Polyclinic.Service.Implementations;

public class IdentityService : IIdentityService
{
    private readonly UserManager<IdentityUser> _userManager;

    private readonly JwtSettings _jwtSettings;

   public IdentityService(UserManager<IdentityUser> userManager, JwtSettings jwtSettings)
    {
        _userManager = userManager;
        _jwtSettings = jwtSettings;
    }

    public async Task<AuthentificationResult> RegisterAsync(string email, string password)
    {
        var existingUser = await _userManager.FindByEmailAsync(email);

        if(existingUser != null)
        {
            return new AuthentificationResult
            {
                Errors = new[] { "User with this email address already exists" }
            };
        }

        var newUser = new IdentityUser
        {
            Email = email,
            UserName = email
        };

        var createdUser = await _userManager.CreateAsync(newUser, password);

        if (!createdUser.Succeeded)
        {
            return new AuthentificationResult
            {
                Errors = createdUser.Errors.Select(x => x.Description)

            };
        }

        var tokenHandler = new JwtSecurityTokenHandler();
        var key = Encoding.UTF8.GetBytes(_jwtSettings.Secret);
        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(new[]
            {
                new Claim(JwtRegisteredClaimNames.Sub, newUser.Email),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim(JwtRegisteredClaimNames.Email, newUser.Email),
                new Claim("id", newUser.Id)
            }),
            Expires = DateTime.UtcNow.AddMinutes(5),
            SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
        };

        var token = tokenHandler.CreateToken(tokenDescriptor);

        return new AuthentificationResult
        {
            Success = true,
            Token = tokenHandler.WriteToken(token)
        };
    }
}
