using Microsoft.AspNetCore.Mvc;
using Polyclinic.Domain.Request;
using Polyclinic.Domain.Response;
using Polyclinic.Service.Interfaces;

namespace Polyclinic.Controllers;

[Route("api/[controller]")]
public class IdentityController : Controller
{
    private readonly IIdentityService _identityService;
    
    public IdentityController(IIdentityService identityService)
    {
        _identityService = identityService;
    }

    [HttpPost("Register")]
    public async Task<IActionResult> Register([FromBody]UserRegistrationRequest request)
    {
        var authResponce = await _identityService.RegisterAsync(request.Email, request.Password);

        if (!authResponce.Success)
        {
            return BadRequest(new AuthFailedResponce
            {
                Errors = authResponce.Errors
            });
        }
        return Ok(new AuthSuccessResponce
        {
            Token = authResponce.Token
        });
    }
}
