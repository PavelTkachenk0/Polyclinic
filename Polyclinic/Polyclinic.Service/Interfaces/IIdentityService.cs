using Polyclinic.Domain.Models;

namespace Polyclinic.Service.Interfaces;

public interface IIdentityService
{
    Task<AuthentificationResult> RegisterAsync(string email, string password);

    Task<AuthentificationResult> LoginAsync(string email, string password);
}
