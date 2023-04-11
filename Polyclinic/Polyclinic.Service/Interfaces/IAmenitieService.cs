using Polyclinic.Domain.Interfaces;
using Polyclinic.Domain.Models;

namespace Polyclinic.Service.Interfaces;
//слой отвечает за получение и обработку данных об услугах из БД
public interface IAmenitieService
{
    Task<IBaseResponce<Amenitie>> GetAmenitieByName(string name);
    Task<IBaseResponce<IEnumerable<Amenitie>>> GetAmenities(); //Получение всех данных обуслугах(всех объектов услуг)
    Task<IBaseResponce<Amenitie>> GetAmenitieById(int id);
    Task<IBaseResponce<bool>> DeleteAmenitie(int id);
    Task<IBaseResponce<Amenitie>> CreateAmenitie(Amenitie amenitie);

}
