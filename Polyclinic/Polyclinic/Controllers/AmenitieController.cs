using Microsoft.AspNetCore.Mvc;
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
    
    [HttpGet]
    public async Task<IEnumerable<Amenitie>> GetAmenities() //получение всех данных
    {
        var responce = await _amenitieService.GetAmenties();
        return responce;
    }   
}