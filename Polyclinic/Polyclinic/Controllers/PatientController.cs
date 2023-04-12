using Microsoft.AspNetCore.Mvc;
using Polyclinic.Domain.Models;
using Polyclinic.Domain.ViewModels;
using Polyclinic.Service.Interfaces;

namespace Polyclinic.Controllers;

[Route("api/[controller]")]
public class PatientController : Controller
{
    private readonly IPatientService _patientService;

    public PatientController(IPatientService patientService)//конструктор класса
    {
        _patientService = patientService;
    }

    [HttpGet("GetAllPatients")]
    public async Task<IEnumerable<Patient>> GetPatients() //получение всех данных
    {
        var response = await _patientService.GetAll();
        return response.Data;
    }

    [HttpGet("GetPatientByPhoneNumber")]
    public async Task<Patient> GetDoctorByPhoneNumber(string phonenUmber)
    {
        var response = await _patientService.GetByPhoneNumber(phonenUmber);
        return response.Data;
    }

    [HttpGet("GetPatientById")]
    public async Task<Patient> GetDoctorById(int id)
    {
        var response = await _patientService.GetById(id);
        return response.Data;
    }

    //[Authorize(Roles = "Admin")]
    [HttpDelete("DeletePatient")]
    public async Task<IActionResult> DeleteDoctor(int id)
    {
        var response = await _patientService.Delete(id);
        return Ok();
    }

    //[Authorize(Roles = "Admin")]
    [HttpPost("CreatePatient")]

    public async Task<IActionResult> CreateDoctor([FromBody] PatientViewModel patient)
    {
        var responce = await _patientService.Create(patient);
        return Ok();
    }
}
