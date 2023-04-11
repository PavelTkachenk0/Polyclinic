using Microsoft.AspNetCore.Mvc;
using Polyclinic.Domain.AmenitieViewModel;
using Polyclinic.Domain.Interfaces;
using Polyclinic.Domain.Models;
using Polyclinic.Domain.Responce;
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
    
    [HttpGet("GetAll")]
    public async Task<IEnumerable<Amenitie>> GetAmenities() //получение всех данных
    {   
        var response = await _amenitieService.GetAmenities();
        return response.Data;
    }

    [HttpGet("GetByName")]
    public async Task<Amenitie> GetAmenitieByName(string name)
    {
        var response = await _amenitieService.GetAmenitieByName(name);
        return response.Data;
    }

    [HttpGet("GetById")]
    public async Task<Amenitie> GetAmenitieById(int id)
    {
        var response = await _amenitieService.GetAmenitieById(id);
        return response.Data;
    }

    //[Authorize(Roles = "Admin")]
    [HttpDelete("Delete")]
    public async Task<IActionResult> DeleteAmenitie(int id)
    {
        var response = await _amenitieService.DeleteAmenitie(id);
        return Ok();
    }

    //[Authorize(Roles = "Admin")]
    [HttpPost("Create")]

    public async Task<IActionResult> CreateAmenitie([FromBody]AmenitieViewModel amenitie)
    {
        var responce = await _amenitieService.CreateAmenitie(amenitie);
        return Ok();
    }
}