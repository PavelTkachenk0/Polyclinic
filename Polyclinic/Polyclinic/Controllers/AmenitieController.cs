using Microsoft.AspNetCore.Mvc;
using Polyclinic.Domain.AmenitieViewModel;
using Polyclinic.Domain.Models;
using Polyclinic.Service.Interfaces;
//контроллер услуг
namespace Polyclinic.Controllers;

[Route("api/[controller]")]
public class AmenitieController : Controller
{
    private readonly IAmenitieService _amenitieService;

    public AmenitieController(IAmenitieService amenitieService)//конструктор класса
    {
        _amenitieService = amenitieService;
    }
    
    [HttpGet("GetAllAmenities")]
    public async Task<IEnumerable<Amenitie>> GetAmenities() //получение всех данных
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
    public async Task<Amenitie> GetAmenitieById(int id)
    {
        var response = await _amenitieService.GetById(id);
        return response.Data;
    }

    //[Authorize(Roles = "Admin")]
    [HttpDelete("DeleteAmenitie")]
    public async Task<IActionResult> DeleteAmenitie(int id)
    {
        var response = await _amenitieService.Delete(id);
        return Ok();
    }

    //[Authorize(Roles = "Admin")]
    [HttpPost("CreateAmenitie")]

    public async Task<IActionResult> CreateAmenitie([FromBody]AmenitieViewModel amenitie)
    {
        var responce = await _amenitieService.Create(amenitie);
        return Ok();
    }
}