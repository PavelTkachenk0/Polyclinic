using Polyclinic.DAL.Interfaces;
using Polyclinic.DAL.Repositories;
using Polyclinic.Domain.AmenitieViewModel;
using Polyclinic.Domain.Enum;
using Polyclinic.Domain.Interfaces;
using Polyclinic.Domain.Models;
using Polyclinic.Domain.Responce;
using Polyclinic.Domain.ViewModels;
using Polyclinic.Service.Interfaces;

namespace Polyclinic.Service.Implementations;

public class DoctorService : IDoctorService
{

    private readonly IDoctorRepository _doctorRepository;//создаем объект репозитория

    public DoctorService(IDoctorRepository doctorRepository)//конструктор
    {
        _doctorRepository = doctorRepository;
    }
    public async Task<IBaseResponce<Doctor>> Create(DoctorViewModel doctor)
    {
        var baseResponce = new BaseResponce<Doctor>();
        try
        {
            var NewDoctor = new Doctor()//инициализация 
            {
                Name = doctor.Name,
                Surname = doctor.Surname,
                MiddleName = doctor.MiddleName,
                Specialization = doctor.Specialization,
            };

            await _doctorRepository.Create(NewDoctor);//создание 
        }
        catch (Exception ex)
        {
            return new BaseResponce<Doctor>()
            {
                Description = $"[CreateDoctor] : {ex.Message}",
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
            var doctor = await _doctorRepository.GetById(id);
            if (doctor == null)//проверяем на ненулевое значение
            {
                baseResponce.Description = "Doctor not found";
                baseResponce.StatusCode = StatusCode.NotFound;

                return baseResponce;
            }
            else
            {
                await _doctorRepository.Delete(doctor);

                return baseResponce;
            }
        }
        catch (Exception ex)
        {
            return new BaseResponce<bool>()
            {
                Description = $"[DeleteDoctor] : {ex.Message}",
                StatusCode = StatusCode.InternalServerError
            };
        }
    }

    public async Task<IBaseResponce<IEnumerable<Doctor>>> GetAll()
    {
        var baseResponce = new BaseResponce<IEnumerable<Doctor>>();
        try
        {
            var doctors = await _doctorRepository.GetAll();//получаем все объекты из таблицы 

            if (doctors.Count == 0)
            {
                baseResponce.Description = "Elements not found";
                baseResponce.StatusCode = StatusCode.NotFound;
                return baseResponce;
            }
            else
            {
                baseResponce.Data = doctors;
                baseResponce.StatusCode = StatusCode.OK;

                return baseResponce;
            }
        }
        catch (Exception ex)//обработка ошибки
        {
            return new BaseResponce<IEnumerable<Doctor>>()
            {
                Description = $"[GetDoctors] : {ex.Message}",
                StatusCode = StatusCode.InternalServerError
            };
        }
    }

    public async Task<IBaseResponce<Doctor>> GetById(int id)
    {
        var baseResponce = new BaseResponce<Doctor>();
        try
        {
            var doctor = await _doctorRepository.GetById(id);//записываем объект по id
            if (doctor == null)//проверяем на ненулевое значение
            {
                baseResponce.Description = "Doctor not found";
                baseResponce.StatusCode = StatusCode.NotFound;

                return baseResponce;
            }
            else
            {
                baseResponce.Data = doctor;//записываем найденный объект в поле Data

                return baseResponce;
            }
        }
        catch (Exception ex) //обработка ошибки
        {
            return new BaseResponce<Doctor>()
            {
                Description = $"[GetDoctorById] : {ex.Message}",
                StatusCode = StatusCode.InternalServerError
            };
        }
    }

    public async Task<IBaseResponce<Doctor>> GetBySpecialization(string specialization)
    {
        var baseResponce = new BaseResponce<Doctor>();
        try
        {
            var doctor = await _doctorRepository.GetBySpecialization(specialization);
            if (doctor == null)
            {
                baseResponce.Description = "Doctor not found";
                baseResponce.StatusCode = StatusCode.NotFound;

                return baseResponce;
            }
            else
            {
                baseResponce.Data = doctor;

                return baseResponce;
            }
        }
        catch (Exception ex)
        {
            return new BaseResponce<Doctor>()
            {
                Description = $"[GetDoctorBySpecialization] : {ex.Message}",
                StatusCode = StatusCode.InternalServerError
            };
        }
    }

    public async Task<IBaseResponce<Doctor>> GetBySurame(string surname)
    {
        var baseResponce = new BaseResponce<Doctor>();
        try
        {
            var doctor = await _doctorRepository.GetBySurname(surname);
            if (doctor == null)
            {
                baseResponce.Description = "Doctor not found";
                baseResponce.StatusCode = StatusCode.NotFound;

                return baseResponce;
            }
            else
            {
                baseResponce.Data = doctor;

                return baseResponce;
            }
        }
        catch (Exception ex)
        {
            return new BaseResponce<Doctor>()
            {
                Description = $"[GetByName] : {ex.Message}",
                StatusCode = StatusCode.InternalServerError
            };
        }
    }
}
