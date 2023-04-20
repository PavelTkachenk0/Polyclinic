using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Polyclinic.Domain.Models;
using Polyclinic.Domain.ViewModels;
using Polyclinic.Service.Interfaces;

namespace Polyclinic.Controllers;

[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "Admin")]

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
    public async Task<Patient> GetPatientByPhoneNumber(string phonenUmber)
    {
        var response = await _patientService.GetByPhoneNumber(phonenUmber);
        return response.Data;
    }

    [HttpGet("GetPatientBySNILS")]
    public async Task<Patient> GetPatientBySNILST(string snils)
    {
        var response = await _patientService.GetBySNILS(snils);
        return response.Data;
    }

    [HttpGet("GetPatientById")]
    public async Task<Patient> GetPatientById(int id)
    {
        var response = await _patientService.GetById(id);
        return response.Data;
    }

    [HttpDelete("DeletePatient")]
    public async Task<IActionResult> DeletePatient(int id)
    {
        var response = await _patientService.Delete(id);
        return Ok();
    }

    [HttpPost("CreatePatient")]

    public async Task<IActionResult> CreatePatient([FromBody] PatientViewModel patient)
    {
        var responce = await _patientService.Create(patient);
        return Ok();
    }
}
