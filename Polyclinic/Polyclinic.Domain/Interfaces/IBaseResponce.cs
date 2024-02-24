using Polyclinic.Domain.Enum;

namespace Polyclinic.Domain.Interfaces;

public interface IBaseResponce<T>
{
    StatusCode StatusCode { get; }
    T Data { get; set; }
}
