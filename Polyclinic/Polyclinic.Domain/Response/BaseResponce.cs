using Polyclinic.Domain.Enum;
using Polyclinic.Domain.Interfaces;
// в папке responce хранятся классы, выступающие в качестве возращаемых объектов для сервисов
namespace Polyclinic.Domain.Responce;
//обработка ответа из базы данных
public class BaseResponce<T> : IBaseResponce<T>
{
    public string Description { get; set; }//описание ошибок

    public StatusCode StatusCode { get; set; }//номер ошибки
        
    public T Data { get; set; } //запись результата ОБРАБОТКИ запроса из бд для передачи в контроллер. Используем дженерик, чтобы добавлять объект произвольного типа

}
