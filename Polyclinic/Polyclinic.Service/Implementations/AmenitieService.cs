using Polyclinic.DAL.Interfaces;
using Polyclinic.Domain.AmenitieViewModel;
using Polyclinic.Domain.Enum;
using Polyclinic.Domain.Interfaces;
using Polyclinic.Domain.Models;
using Polyclinic.Domain.Responce;
using Polyclinic.Service.Interfaces;
//реализуем обработку данных об услугах из БД
namespace Polyclinic.Service.Implementations;

public class AmenitieService : IAmenitieService
{
    private readonly IAmenitieRepository _amenitieRepository;//создаем объект репозитория

    public AmenitieService(IAmenitieRepository amenitieRepository)//конструктор
    {
        _amenitieRepository = amenitieRepository;
    }

    public async Task<IBaseResponse<Amenitie>> Create(AmenitieViewModel amenitie)
    {
    var baseResponce = new BaseResponse<Amenitie>(); 
        try
        {
            var NewAmenitie = new Amenitie()//инициализация услуги
            {
                Name = amenitie.Name,
                Description = amenitie.Description,
                StartOfReception = amenitie.StartOfReception,
                EndOfReception = amenitie.EndOfReception
            };

            await _amenitieRepository.Create(NewAmenitie);//создание 
        }
        catch (Exception ex)
        {
            return new BaseResponse<Amenitie>()
            {
                Description = $"[CreateAmenitie] : {ex.Message}",
                StatusCode = StatusCode.InternalServerError
            };
        }
        return baseResponce;
    }

    public async Task<IBaseResponse<Amenitie>> Edit(int id, AmenitieViewModel amenitie)
    {
        var baseResponse = new BaseResponse<Amenitie>();
        try
        {
            var NewAmenitie = await _amenitieRepository.GetById(id);
            if (NewAmenitie == null)
            {
                baseResponse.StatusCode = StatusCode.NotFound;
                baseResponse.Description = "Amenitie not found";
                return baseResponse;
            }

            NewAmenitie.Name = amenitie.Name;
            NewAmenitie.Description = amenitie.Description;
            NewAmenitie.StartOfReception = amenitie.StartOfReception;
            NewAmenitie.EndOfReception = amenitie.EndOfReception;
           

            await _amenitieRepository.Update(NewAmenitie);

            return baseResponse;

        }
        catch (Exception ex)
        {
            return new BaseResponse<Amenitie>()
            {
                Description = $"[Edit] : {ex.Message}",
                StatusCode = StatusCode.InternalServerError
            };
        }
    }


    public async Task<IBaseResponse<Amenitie>> GetById(int id)//получить услугу по id
    {
        var baseResponce = new BaseResponse<Amenitie>();
        try
        {
            var amenitie = await _amenitieRepository.GetById(id);//записываем объект по id
            if(amenitie == null)//проверяем на ненулевое значение
            {
                baseResponce.Description = "Amenitie not found";
                baseResponce.StatusCode = StatusCode.NotFound;

                return baseResponce;
            }
            else
            {
                baseResponce.Data = amenitie;//записываем найденный объект в поле Data

                return baseResponce;
            }
        }
        catch(Exception ex) //обработка ошибки
        {
            return new BaseResponse<Amenitie>()
            {
                Description = $"[GetAmenitieById] : {ex.Message}",
                StatusCode = StatusCode.InternalServerError
            };
        }
    }

    public async Task<IBaseResponse<Amenitie>> GetByName(string name)//получить услугу по имени
    {
        var baseResponce = new BaseResponse<Amenitie>();
        try
        {
            var amenitie = await _amenitieRepository.GetByName(name);
            if (amenitie == null)
            {
                baseResponce.Description = "Amenitie not found";
                baseResponce.StatusCode = StatusCode.NotFound;

                return baseResponce;
            }
            else
            {
                baseResponce.Data = amenitie;

                return baseResponce;
            }
        }
        catch (Exception ex)
        {
            return new BaseResponse<Amenitie>()
            {
                Description = $"[GetAmenitieByName] : {ex.Message}",
                StatusCode = StatusCode.InternalServerError
            };
        }
    }
    public async Task<IBaseResponse<IEnumerable<Amenitie>>> GetAll()//получить список услуг 
    {
        var baseResponce = new BaseResponse<IEnumerable<Amenitie>>();
        try
        {
            var amenities = await _amenitieRepository.GetAll();//получаем все объекты из таблицы 

            if(amenities.Count == 0)
            {
                baseResponce.Description = "Elements not found";
                baseResponce.StatusCode = StatusCode.NotFound;
                return baseResponce;
            }
            else
            {
                baseResponce.Data = amenities;
                baseResponce.StatusCode = StatusCode.OK;

                return baseResponce;
            }
        }
        catch (Exception ex)//обработка ошибки
        {
            return new BaseResponse<IEnumerable<Amenitie>>()
            {
                Description = $"[GetAmenities] : {ex.Message}",
                StatusCode = StatusCode.InternalServerError
            };
        }
    }

    public async Task<IBaseResponse<bool>> Delete(int id)//удаление услуги 
    {
        var baseResponce = new BaseResponse<bool>();
        try
        {
            var amenitie = await _amenitieRepository.GetById(id);
            if (amenitie == null)//проверяем на ненулевое значение
            {
                baseResponce.Description = "Amenitie not found";
                baseResponce.StatusCode = StatusCode.NotFound;

                return baseResponce;
            }
            else
            {
                await _amenitieRepository.Delete(amenitie);

                return baseResponce;
            }
        }
        catch (Exception ex)
        {
            return new BaseResponse<bool>()
            {
                Description = $"[DeleteAmenitie] : {ex.Message}",
                StatusCode = StatusCode.InternalServerError
            };
        }
    }
}
