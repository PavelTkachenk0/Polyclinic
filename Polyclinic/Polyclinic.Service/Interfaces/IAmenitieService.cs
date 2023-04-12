using Polyclinic.Domain.AmenitieViewModel;
using Polyclinic.Domain.Interfaces;
using Polyclinic.Domain.Models;

namespace Polyclinic.Service.Interfaces;
//слой отвечает за получение и обработку данных об услугах из БД
public interface IAmenitieService : IService<Amenitie, AmenitieViewModel>
{
    Task<IBaseResponce<Amenitie>> GetByName(string name);

}
