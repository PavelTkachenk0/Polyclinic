using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Polyclinic.Domain.AmenitieViewModel;
using Polyclinic.Domain.Models;
using Polyclinic.Domain.ViewModels;
using Polyclinic.Service.Interfaces;
using System.Data;

namespace Polyclinic.Controllers;

[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "Admin, User")]

[Route("api/[controller]")]
public class DoctorController : Controller
{
    private readonly IDoctorService _doctorService;

    public DoctorController(IDoctorService doctorService)//конструктор класса
    {
        _doctorService = doctorService;
    }

    [HttpGet("GetAllDoctors")]
    public async Task<IEnumerable<Doctor>> GetDoctors() //получение всех данных
    {
        var response = await _doctorService.GetAll();
        return response.Data;
    }

    [HttpGet("GetDoctorByName")]
    public async Task<Doctor> GetDoctorBySurame(string surname)
    {
        var response = await _doctorService.GetBySurame(surname);
        return response.Data;
    }

    [HttpGet("GetDoctorById")]
    [Authorize(Roles = "Admin")]
    public async Task<Doctor> GetDoctorById(int id)
    {
        var response = await _doctorService.GetById(id);
        return response.Data;
    }

    [HttpDelete("DeleteDoctors")]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> DeleteDoctor(int id)
    {
        var response = await _doctorService.Delete(id);
        return Ok();
    }

    [HttpPost("CreateDoctors")]
    [Authorize(Roles = "Admin")]

    public async Task<IActionResult> CreateDoctor([FromBody] DoctorViewModel doctor)
    {
        var responce = await _doctorService.Create(doctor);
        return Ok();
    }
}
