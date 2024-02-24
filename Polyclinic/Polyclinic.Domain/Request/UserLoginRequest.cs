namespace Polyclinic.Domain.Request;
//описание полей для логина пользователя
public class UserLoginRequest
{
    public string Email { get; set; }

    public string Password { get; set; }
}
