using Polyclinic.DAL.Interfaces;
using Polyclinic.Domain.Enum;
using Polyclinic.Domain.Interfaces;
using Polyclinic.Domain.Models;
using Polyclinic.Domain.Responce;
using Polyclinic.Domain.ViewModels;
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

    public async Task<IBaseResponce<AmenitieViewModel>> CreateAmenitie(AmenitieViewModel amenitieViewModel)//добавить услугу
    {
        var baseResponce = new BaseResponce<AmenitieViewModel>(); 
        try
        {
            var amenitie = new Amenitie()//инициализация услуги
            {
                Name = amenitieViewModel.Name,
                Description = amenitieViewModel.Description,
                StartOfReception = DateTime.Now,
                EndOfReception = DateTime.Now
            };

            await _amenitieRepository.Create(amenitie);//создание 
        }
        catch (Exception ex)
        {
            return new BaseResponce<AmenitieViewModel>()
            {
                Description = $"[CreateAmenitie] : {ex.Message}",
                StatusCode = StatusCode.InternalServerError
            };
        }
        return baseResponce;
    }

    public async Task<IBaseResponce<Amenitie>> GetAmenitieById(int id)//получить услугу по id
    {
        var baseResponce = new BaseResponce<Amenitie>();
        try
        {
            var amenitie = await _amenitieRepository.GetById(id);//записываем объект по id
            if(amenitie == null)//проверяем на ненулевое значение
            {
                baseResponce.Description = "Amenitie not found";
                baseResponce.StatusCode = StatusCode.AmenitieNotFound;

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

    public async Task<IBaseResponce<Amenitie>> GetAmenitieByName(string name)//получить услугу по имени
    {
        var baseResponce = new BaseResponce<Amenitie>();
        try
        {
            var amenitie = await _amenitieRepository.GetByName(name);
            if (amenitie == null)
            {
                baseResponce.Description = "Amenitie not found";
                baseResponce.StatusCode = StatusCode.AmenitieNotFound;

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

    public async Task<IEnumerable<Amenitie>> GetAmenties()//получить список услуг 
    {
        try
        {
            var amenities = await _amenitieRepository.GetAll();//получаем все объекты из таблицы 
            return amenities;
        }
        catch (Exception ex)//обработка ошибки
        {
            return null;
        }
    }

    public async Task<IBaseResponce<bool>> DeleteAmenitie(int id)//удаление услуги 
    {
        var baseResponce = new BaseResponce<bool>();
        try
        {
            var amenitie = await _amenitieRepository.GetById(id);
            if (amenitie == null)//проверяем на ненулевое значение
            {
                baseResponce.Description = "Amenitie not found";
                baseResponce.StatusCode = StatusCode.AmenitieNotFound;

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
                Description = $"[DeleteCar] : {ex.Message}",
                StatusCode = StatusCode.InternalServerError
            };
        }
    }
}
