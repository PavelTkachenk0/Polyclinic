namespace Polyclinic.Domain.Models;

public class AuthentificationResult
{
    public string Token { get; set; }

    public bool Success { get; set; }

    public IEnumerable<string> Errors { get; set; }
}
