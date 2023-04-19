using Microsoft.AspNetCore.Identity;
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

    private readonly RoleManager<IdentityRole> _roleManager;

    private readonly JwtSettings _jwtSettings;

    public IdentityService(UserManager<IdentityUser> userManager, JwtSettings jwtSettings, RoleManager<IdentityRole> roleManager)
    {
        _userManager = userManager;
        _jwtSettings = jwtSettings;
        _roleManager = roleManager;
    }

    public async Task<AuthentificationResult> RegisterAsync(string email, string password)
    {
        var existingUser = await _userManager.FindByEmailAsync(email);

        if (existingUser != null)
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

        if (_userManager.FindByEmailAsync("admin@gmail.com").Result == null)
        {
            var createdAdmin = await _userManager.CreateAsync(newUser, password);
            if (!createdAdmin.Succeeded)
            {
                return new AuthentificationResult
                {
                    Errors = createdAdmin.Errors.Select(x => x.Description)

                };
            }
            _userManager.AddToRoleAsync(newUser, "Admin").Wait();
        }

        if (_userManager.FindByEmailAsync("admin@gmail.com").Result != null)
        {
            var createdUser = await _userManager.CreateAsync(newUser, password);
            if (!createdUser.Succeeded)
            {
                return new AuthentificationResult
                {
                    Errors = createdUser.Errors.Select(x => x.Description)

                };
            }
            _userManager.AddToRoleAsync(newUser, "User").Wait();
        }

        return await GenerateAuthentificationResultForUserAsync(newUser);
    }

    public async Task<AuthentificationResult> LoginAsync(string email, string password)
    {
        var user = await _userManager.FindByEmailAsync(email);

        if (user == null)
        {
            return new AuthentificationResult
            {
                Errors = new[] { "User does not exist" }
            };
        }

        var userHasValidPassword = await _userManager.CheckPasswordAsync(user, password);

        if (!userHasValidPassword)
        {
            return new AuthentificationResult
            {
                Errors = new[] { "User/password combination is wrong" }
            };
        }

        return await GenerateAuthentificationResultForUserAsync(user);
    }

    public async Task<AuthentificationResult> GenerateAuthentificationResultForUserAsync(IdentityUser user)
    {
        var tokenHandler = new JwtSecurityTokenHandler();
        var key = Encoding.UTF8.GetBytes(_jwtSettings.Secret);

        var claims = new List<Claim>
        {

            new Claim(JwtRegisteredClaimNames.Sub, user.Email),
            new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
            new Claim(JwtRegisteredClaimNames.Email, user.Email),
            new Claim("id", user.Id)
        };

        var userRoles = await _userManager.GetRolesAsync(user);
        foreach (var role in userRoles)
        {
            claims.Add(new Claim(ClaimTypes.Role, role));
        }

        var userClaims = await _userManager.GetClaimsAsync(user);
        claims.AddRange(userClaims);
        
        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(claims),
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

