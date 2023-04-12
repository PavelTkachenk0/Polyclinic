using Polyclinic.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace Polyclinic.DAL;
//подключение базы данных в проект
public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
    {
        //Database.EnsureDeleted();
        //Database.EnsureCreated();
    }

    public DbSet<Amenitie> Amenitie { get; set; }//сущность для получения данных из таблицы услуг
    public DbSet<Doctor> Doctor { get; set; }
    public DbSet<Patient> Patient { get; set; }
}