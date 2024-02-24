using Polyclinic.DAL.Interfaces;
using Polyclinic.DAL.Repositories;
using Polyclinic.Domain.AmenitieViewModel;
using Polyclinic.Domain.Enum;
using Polyclinic.Domain.Interfaces;
using Polyclinic.Domain.Models;
using Polyclinic.Domain.Responce;
using Polyclinic.Domain.ViewModels;
using Polyclinic.Service.Interfaces;
using System.Numerics;

namespace Polyclinic.Service.Implementations;

public class DoctorService : IDoctorService
{

    private readonly IDoctorRepository _doctorRepository;//создаем объект репозитория

    public DoctorService(IDoctorRepository doctorRepository)//конструктор
    {
        _doctorRepository = doctorRepository;
    }
    public async Task<IBaseResponse<Doctor>> Create(DoctorViewModel doctor)
    {
        var baseResponce = new BaseResponse<Doctor>();
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
            return new BaseResponse<Doctor>()
            {
                Description = $"[CreateDoctor] : {ex.Message}",
                StatusCode = StatusCode.InternalServerError
            };
        }
        return baseResponce;
    }

    public async Task<IBaseResponse<Doctor>> Edit(int id, DoctorViewModel doctor)
    {
        var baseResponse = new BaseResponse<Doctor>();
        try
        {
            var NewDoctor = await _doctorRepository.GetById(id);
            if (NewDoctor == null)
            {
                baseResponse.StatusCode = StatusCode.NotFound;
                baseResponse.Description = "Doctor not found";
                return baseResponse;
            }

            NewDoctor.Name = doctor.Name;
            NewDoctor.Surname = doctor.Surname;
            NewDoctor.MiddleName = doctor.MiddleName;
            NewDoctor.Specialization = doctor.Specialization;


            await _doctorRepository.Update(NewDoctor);

            return baseResponse;

        }
        catch (Exception ex)
        {
            return new BaseResponse<Doctor>()
            {
                Description = $"[Edit] : {ex.Message}",
                StatusCode = StatusCode.InternalServerError
            };
        }
    }

    public async Task<IBaseResponse<bool>> Delete(int id)
    {
        var baseResponce = new BaseResponse<bool>();
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
            return new BaseResponse<bool>()
            {
                Description = $"[DeleteDoctor] : {ex.Message}",
                StatusCode = StatusCode.InternalServerError
            };
        }
    }

    public async Task<IBaseResponse<IEnumerable<Doctor>>> GetAll()
    {
        var baseResponce = new BaseResponse<IEnumerable<Doctor>>();
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
            return new BaseResponse<IEnumerable<Doctor>>()
            {
                Description = $"[GetDoctors] : {ex.Message}",
                StatusCode = StatusCode.InternalServerError
            };
        }
    }

    public async Task<IBaseResponse<Doctor>> GetById(int id)
    {
        var baseResponce = new BaseResponse<Doctor>();
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
            return new BaseResponse<Doctor>()
            {
                Description = $"[GetDoctorById] : {ex.Message}",
                StatusCode = StatusCode.InternalServerError
            };
        }
    }

    public async Task<IBaseResponse<Doctor>> GetBySpecialization(string specialization)//получаем врача по специальности
    {
        var baseResponce = new BaseResponse<Doctor>();
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
            return new BaseResponse<Doctor>()
            {
                Description = $"[GetDoctorBySpecialization] : {ex.Message}",
                StatusCode = StatusCode.InternalServerError
            };
        }
    }

    public async Task<IBaseResponse<Doctor>> GetBySurame(string surname)//получаем врача по фамилии
    {
        var baseResponce = new BaseResponse<Doctor>();
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
            return new BaseResponse<Doctor>()
            {
                Description = $"[GetByName] : {ex.Message}",
                StatusCode = StatusCode.InternalServerError
            };
        }
    }
}
