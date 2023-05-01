using Polyclinic.Domain.AmenitieViewModel;
using Polyclinic.Domain.Interfaces;
using Polyclinic.Domain.Models;
using Polyclinic.Domain.Responce;

namespace Polyclinic.Service.Interfaces;

public interface IService<T, M>
{
    Task<IBaseResponse<IEnumerable<T>>> GetAll();
    Task<IBaseResponse<T>> GetById(int id);
    Task<IBaseResponse<bool>> Delete(int id);
    Task<IBaseResponse<T>> Create(M entity);

    Task<IBaseResponse<T>> Edit(int id, M entity);
}
