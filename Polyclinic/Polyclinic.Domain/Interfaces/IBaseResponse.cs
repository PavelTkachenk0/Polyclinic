using Polyclinic.Domain.Enum;

namespace Polyclinic.Domain.Interfaces;
//интерфейс класса BaseResponce(см.Domain.Responce.BaseResponce)
public interface IBaseResponse<T>
{
    StatusCode StatusCode { get; }
    T Data { get; set; }
}
