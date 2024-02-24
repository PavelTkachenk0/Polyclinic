namespace Polyclinic.DAL.Interfaces;
//общий интерфейс
//репозиторий - класс, содержащий методы взаимодействия с бд
public interface IBaseRepository<T>//дженерик, принимающий какой-либо объект
{
    Task<bool> Create(T entity); //добавление записи в таблицу

    Task<T> GetById(int id);  //получение объекта по id

    Task<List<T>> GetAll(); //получить все данные таблицы(в виде коллекции элементов)

    Task<bool> Delete(T entity); //удалить запись из таблицы
}
