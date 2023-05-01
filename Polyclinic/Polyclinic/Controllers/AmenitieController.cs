using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Polyclinic.Domain.AmenitieViewModel;
using Polyclinic.Domain.Models;
using Polyclinic.Service.Interfaces;
using System.Data;
//контроллер услуг
namespace Polyclinic.Controllers;

[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "Admin, User")]

[Route("api/[controller]")]
public class AmenitieController : Controller
{
    private readonly IAmenitieService _amenitieService;

    public AmenitieController(IAmenitieService amenitieService)
    {
        _amenitieService = amenitieService;
    }
    
    [HttpGet("GetAllAmenities")]
    public async Task<IEnumerable<Amenitie>> GetAmenities() 
    {   
        var response = await _amenitieService.GetAll();
        return response.Data;
    }

    [HttpGet("GetAmenitieByName")]
    public async Task<Amenitie> GetAmenitieByName(string name)
    {
        var response = await _amenitieService.GetByName(name);
        return response.Data;
    }

    [HttpGet("GetAmenitieById")]
    [Authorize(Roles = "Admin")]
    public async Task<Amenitie> GetAmenitieById(int id)
    {
        var response = await _amenitieService.GetById(id);
        return response.Data;
    }

    [HttpDelete("DeleteAmenitie")]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> DeleteAmenitie(int id)
    {
        var response = await _amenitieService.Delete(id);
        return Ok();
    }

    [HttpPost("CreateAmenitie")]
    [Authorize(Roles = "Admin")]

    public async Task<IActionResult> CreateAmenitie([FromBody]AmenitieViewModel amenitie)
    {
        var responce = await _amenitieService.Create(amenitie);
        return Ok();
    }

    public async Task<IActionResult> UpdateAmenitie([FromBody] AmenitieViewModel amenitie)
    {
        var responce = await _amenitieService.Edit(amenitie.Id, amenitie);
        return Ok();
    }
}