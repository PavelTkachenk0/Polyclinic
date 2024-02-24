using Microsoft.EntityFrameworkCore;
using Polyclinic.DAL.Interfaces;
using Polyclinic.Domain.Models;

namespace Polyclinic.DAL.Repositories;
//реализация репозитория услуг  
public class AmenitieRepository : IAmenitieRepository
{
    private readonly ApplicationDbContext _db; //создаем объект класса AbdConcext для обращения к базе данных(к сущность, которая реализована в DbContext)

    public AmenitieRepository(ApplicationDbContext db)
    {
        _db = db;
    }

    public async Task<bool> Create(Amenitie entity)//Добавление объекта в БД
    {
       await _db.Amenitie.AddAsync(entity);
       await _db.SaveChangesAsync();
       return true;
    }

    public async Task<bool> Delete(Amenitie entity)//удаление обычекта из БД
    {
        _db.Amenitie.Remove(entity);
        await _db.SaveChangesAsync();
        return true;
    }

    public async Task<Amenitie> GetById(int id)
    {
        return await _db.Amenitie.FirstOrDefaultAsync(x => x.Id == id);//возвращает первое найденное значение, удовлетворяющее условию(id)
    }

    public async Task<List<Amenitie>> GetAll() //получение списка услуг в коллекцию List
    {
        return await _db.Amenitie.ToListAsync();
    }

    public async Task<Amenitie> GetByName(string name)//возвращает первое найденное значение, удовлетворяющее условию(name)
    {
        return await _db.Amenitie.FirstOrDefaultAsync(x => x.Name == name);
    }
}