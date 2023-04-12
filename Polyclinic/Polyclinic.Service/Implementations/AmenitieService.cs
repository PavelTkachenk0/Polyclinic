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

    public async Task<IBaseResponce<Amenitie>> Create(AmenitieViewModel amenitie)
    {
    var baseResponce = new BaseResponce<Amenitie>(); 
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
            return new BaseResponce<Amenitie>()
            {
                Description = $"[CreateAmenitie] : {ex.Message}",
                StatusCode = StatusCode.InternalServerError
            };
        }
        return baseResponce;
    }

    public async Task<IBaseResponce<Amenitie>> GetById(int id)//получить услугу по id
    {
        var baseResponce = new BaseResponce<Amenitie>();
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
            return new BaseResponce<Amenitie>()
            {
                Description = $"[GetAmenitieById] : {ex.Message}",
                StatusCode = StatusCode.InternalServerError
            };
        }
    }

    public async Task<IBaseResponce<Amenitie>> GetByName(string name)//получить услугу по имени
    {
        var baseResponce = new BaseResponce<Amenitie>();
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
            return new BaseResponce<Amenitie>()
            {
                Description = $"[GetAmenitieByName] : {ex.Message}",
                StatusCode = StatusCode.InternalServerError
            };
        }
    }
    public async Task<IBaseResponce<IEnumerable<Amenitie>>> GetAll()//получить список услуг 
    {
        var baseResponce = new BaseResponce<IEnumerable<Amenitie>>();
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
            return new BaseResponce<IEnumerable<Amenitie>>()
            {
                Description = $"[GetAmenities] : {ex.Message}",
                StatusCode = StatusCode.InternalServerError
            };
        }
    }

    public async Task<IBaseResponce<bool>> Delete(int id)//удаление услуги 
    {
        var baseResponce = new BaseResponce<bool>();
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
            return new BaseResponce<bool>()
            {
                Description = $"[DeleteAmenitie] : {ex.Message}",
                StatusCode = StatusCode.InternalServerError
            };
        }
    }
}
