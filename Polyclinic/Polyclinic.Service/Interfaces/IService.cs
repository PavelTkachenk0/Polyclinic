using Polyclinic.Domain.AmenitieViewModel;
using Polyclinic.Domain.Interfaces;
using Polyclinic.Domain.Models;

namespace Polyclinic.Service.Interfaces;

public interface IService<T, M>
{
    Task<IBaseResponce<IEnumerable<T>>> GetAll();
    Task<IBaseResponce<T>> GetById(int id);
    Task<IBaseResponce<bool>> Delete(int id);
    Task<IBaseResponce<T>> Create(M entity);
}
