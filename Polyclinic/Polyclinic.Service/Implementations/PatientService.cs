using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Polyclinic.DAL.Interfaces;
using Polyclinic.Domain.Enum;
using Polyclinic.Domain.Interfaces;
using Polyclinic.Domain.Models;
using Polyclinic.Domain.Responce;
using Polyclinic.Domain.ViewModels;
using Polyclinic.Service.Interfaces;

namespace Polyclinic.Service.Implementations;

[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
public class PatientService : IPatientService
{
    private readonly IPatientRepository _patientRepository;//создаем объект репозитория

    public PatientService(IPatientRepository patientRepository)//конструктор
    {
        _patientRepository = patientRepository;
    }
    public async Task<IBaseResponce<Patient>> Create(PatientViewModel patient)
    {
        var baseResponce = new BaseResponce<Patient>();
        try
        {
            var NewPatient = new Patient()//инициализация 
            {
                Name = patient.Name,
                Surname = patient.Surname,
                MiddleName = patient.MiddleName,
                PhoneNumber = patient.PhoneNumber,
                SNILS = patient.SNILS,
            };

            await _patientRepository.Create(NewPatient);//создание 
        }
        catch (Exception ex)
        {
            return new BaseResponce<Patient>()
            {
                Description = $"[CreatePatient] : {ex.Message}",
                StatusCode = StatusCode.InternalServerError
            };
        }
        return baseResponce;
    }

    public async Task<IBaseResponce<bool>> Delete(int id)
    {
        var baseResponce = new BaseResponce<bool>();
        try
        {
            var patient = await _patientRepository.GetById(id);
            if (patient == null)//проверяем на ненулевое значение
            {
                baseResponce.Description = "Patient not found";
                baseResponce.StatusCode = StatusCode.NotFound;

                return baseResponce;
            }
            else
            {
                await _patientRepository.Delete(patient);

                return baseResponce;
            }
        }
        catch (Exception ex)
        {
            return new BaseResponce<bool>()
            {
                Description = $"[DeletePatient] : {ex.Message}",
                StatusCode = StatusCode.InternalServerError
            };
        }
    }

    public async Task<IBaseResponce<IEnumerable<Patient>>> GetAll()
    {
        var baseResponce = new BaseResponce<IEnumerable<Patient>>();
        try
        {
            var patient = await _patientRepository.GetAll();//получаем все объекты из таблицы 

            if (patient.Count == 0)
            {
                baseResponce.Description = "Elements not found";
                baseResponce.StatusCode = StatusCode.NotFound;
                return baseResponce;
            }
            else
            {
                baseResponce.Data = patient;
                baseResponce.StatusCode = StatusCode.OK;

                return baseResponce;
            }
        }
        catch (Exception ex)//обработка ошибки
        {
            return new BaseResponce<IEnumerable<Patient>>()
            {
                Description = $"[GetPatients] : {ex.Message}",
                StatusCode = StatusCode.InternalServerError
            };
        }
    }

    public async Task<IBaseResponce<Patient>> GetById(int id)
    {
        var baseResponce = new BaseResponce<Patient>();
        try
        {
            var patient = await _patientRepository.GetById(id);//записываем объект по id
            if (patient == null)//проверяем на ненулевое значение
            {
                baseResponce.Description = "Patient not found";
                baseResponce.StatusCode = StatusCode.NotFound;

                return baseResponce;
            }
            else
            {
                baseResponce.Data = patient;//записываем найденный объект в поле Data

                return baseResponce;
            }
        }
        catch (Exception ex) //обработка ошибки
        {
            return new BaseResponce<Patient>()
            {
                Description = $"[GetPatientById] : {ex.Message}",
                StatusCode = StatusCode.InternalServerError
            };
        }
    }

    public async Task<IBaseResponce<Patient>> GetByPhoneNumber(string phoneNumber)
    {
        var baseResponce = new BaseResponce<Patient>();
        try
        {
            var patient = await _patientRepository.GetByPhoneNumber(phoneNumber);
            if (patient == null)
            {
                baseResponce.Description = "Patient not found";
                baseResponce.StatusCode = StatusCode.NotFound;

                return baseResponce;
            }
            else
            {
                baseResponce.Data = patient;

                return baseResponce;
            }
        }
        catch (Exception ex)
        {
            return new BaseResponce<Patient>()
            {
                Description = $"[GetPatientBySpecialization] : {ex.Message}",
                StatusCode = StatusCode.InternalServerError
            };
        }
    }

    public async Task<IBaseResponce<Patient>> GetBySNILS(string snils)
    {
        var baseResponce = new BaseResponce<Patient>();
        try
        {
            var patient = await _patientRepository.GetBySNILS(snils);
            if (patient == null)
            {
                baseResponce.Description = "Patient not found";
                baseResponce.StatusCode = StatusCode.NotFound;

                return baseResponce;
            }
            else
            {
                baseResponce.Data = patient;

                return baseResponce;
            }
        }
        catch (Exception ex)
        {
            return new BaseResponce<Patient>()
            {
                Description = $"[GetByName] : {ex.Message}",
                StatusCode = StatusCode.InternalServerError
            };
        }
    }
}
