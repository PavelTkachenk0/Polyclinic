using Polyclinic.Domain.Interfaces;
using Polyclinic.Domain.Models;

namespace Polyclinic.Service.Interfaces;
//слой отвечает за получение и обработку данных об услугах из БД
public interface IAmenitieService
{
    Task<IEnumerable<Amenitie>> GetAmenties(); //Получение всех данных обуслугах(всех объектов услуг)
}
