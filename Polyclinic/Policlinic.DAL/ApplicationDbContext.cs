using Polyclinic.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;

namespace Polyclinic.DAL;
//подключение базы данных в проект
public class ApplicationDbContext : IdentityDbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
    {
        Database.EnsureDeleted();
        Database.EnsureCreated();
    }

    public DbSet<Amenitie> Amenitie { get; set; }

    public DbSet<Doctor> Doctor { get; set; }

    public DbSet<Patient> Patient { get; set; }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        builder.Entity<IdentityRole>().HasData(
            new IdentityRole[]{
            new IdentityRole {Name = "Admin"},
            new IdentityRole {Name = "User"}
            }
            );
    }
}