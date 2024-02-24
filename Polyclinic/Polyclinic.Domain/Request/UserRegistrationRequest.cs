using System.ComponentModel.DataAnnotations;

namespace Polyclinic.Domain.Request;

public class UserRegistrationRequest
{
    [EmailAddress]
    public string Email { get; set; }

    public string Password { get; set; }
}