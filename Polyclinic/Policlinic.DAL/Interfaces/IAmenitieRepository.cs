using Polyclinic.Domain.Models;
//интерфейс для услуг
namespace Polyclinic.DAL.Interfaces;

public interface IAmenitieRepository : IBaseRepository<Amenitie>
{
    Task<Amenitie> GetByName(string name); //получить услугу по названию
}
