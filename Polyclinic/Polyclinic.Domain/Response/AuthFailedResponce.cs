namespace Polyclinic.Domain.Response;
//список ошибок при сбое аутентификации
public class AuthFailedResponce
{
    public IEnumerable<string> Errors { get; set; }
}
