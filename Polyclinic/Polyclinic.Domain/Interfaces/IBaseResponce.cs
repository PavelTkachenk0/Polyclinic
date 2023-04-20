using Polyclinic.Domain.Enum;

namespace Polyclinic.Domain.Interfaces;
//интерфейс класса BaseResponce(см.Domain.Responce.BaseResponce)
public interface IBaseResponce<T>
{
    StatusCode StatusCode { get; }
    T Data { get; set; }
}
