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
        if (!ModelState.IsValid)
        {
            return BadRequest(new AuthFailedResponce
            {
                Errors = ModelState.Values.SelectMany(x => x.Errors.Select(y => y.ErrorMessage))
        });
        }

        var authResponse = await _identityService.RegisterAsync(request.Email, request.Password);

        if (!authResponse.Success)
        {
            return BadRequest(new AuthFailedResponce
            {
                Errors = authResponse.Errors
            });
        }
        return Ok(new AuthSuccessResponce
        {
            Token = authResponse.Token
        });
    }

    [HttpPost("Login")]

    public async Task<IActionResult> Login([FromBody]UserLoginRequest request)
    {
        var authResponse = await _identityService.LoginAsync(request.Email, request.Password);

        if(!authResponse.Success)
        {
            return BadRequest(new AuthFailedResponce
            {
                Errors = authResponse.Errors
            });
        }
         
        return Ok(new AuthSuccessResponce
        {
            Token = authResponse.Token
        });
    }
}
