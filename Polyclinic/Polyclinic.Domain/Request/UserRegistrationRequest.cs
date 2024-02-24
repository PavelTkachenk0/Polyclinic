using System.ComponentModel.DataAnnotations;

namespace Polyclinic.Domain.Request;
//описание полей для регистрации пользователя
public class UserRegistrationRequest
{
    [EmailAddress]
    public string Email { get; set; }

    public string Password { get; set; }
}