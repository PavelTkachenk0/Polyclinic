using Microsoft.EntityFrameworkCore;
using Polyclinic.DAL;
using Polyclinic.DAL.Interfaces;
using Polyclinic.DAL.Repositories;
using Polyclinic.Service.Implementations;
using Polyclinic.Service.Interfaces;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

//соединяемся с базой данных
var connection = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(connection));

//подключаем необходимые библиотеки в проект
builder.Services.AddScoped<IAmenitieRepository, AmenitieRepository>();
builder.Services.AddScoped<IAmenitieService, AmenitieService>();

builder.Services.AddSwaggerGen();

var app = builder.Build();

// The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
app.UseDeveloperExceptionPage();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.UseStaticFiles();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseEndpoints(options =>
{
    options.MapControllers();
    options.MapDefaultControllerRoute();
});

app.Run();
