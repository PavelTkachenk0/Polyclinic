using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Polyclinic.DAL;
using Polyclinic.DAL.Interfaces;
using Polyclinic.DAL.Repositories;
using Polyclinic.Domain.Settings;
using Polyclinic.Service.Implementations;
using Polyclinic.Service.Interfaces;
using System.Configuration;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

var connection = builder.Configuration.GetConnectionString("AppDbContext");
//builder.Services.AddDbContext<ApplicationDbContext>(options =>
//    options.UseSqlServer(connection));
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseNpgsql(connection));

builder.Services.AddDefaultIdentity<IdentityUser>()
    .AddRoles<IdentityRole>()
    .AddEntityFrameworkStores<ApplicationDbContext>();

var jwtSettings = new JwtSettings();

builder.Services.AddSingleton(jwtSettings);

builder.Services.Configure<JwtSettings>(builder.Configuration.GetSection("JwtSettings"));


builder.Services.AddAuthentication(x =>
{
    x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme; ;
    x.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
    x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
    .AddJwtBearer(x =>
    {
        x.SaveToken = true;
        x.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSettings.Secret)),
            ValidateIssuer = false,
            ValidateAudience = false,
            RequireExpirationTime = false,
            ValidateLifetime = true
        };
    });

builder.Services.AddSwaggerGen(x =>
{
    var security = new Dictionary<string, IEnumerable<string>>
    {
        {"Bearer", new string[0]}
    };

    x.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Description = "JWT Authorization header using the bearer scheme",
        Name = "Authorization",
        In = ParameterLocation.Header,
        Type = SecuritySchemeType.ApiKey,
        Scheme = "Bearer"
    });

    x.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                 Reference = new OpenApiReference
                 {
                      Type = ReferenceType.SecurityScheme,
                      Id = "Bearer"
                 }

            },
            new string[] {}
        }
    });
});

builder.Services.AddOptions();
  
builder.Services.AddScoped<IAmenitieRepository, AmenitieRepository>();
builder.Services.AddScoped<IDoctorRepository, DoctorRepository>();
builder.Services.AddScoped<IPatientRepository, PatientRepository>();
builder.Services.AddScoped<IIdentityService, IdentityService>();

builder.Services.AddScoped<IAmenitieService, AmenitieService>();
builder.Services.AddScoped<IDoctorService, DoctorService>();
builder.Services.AddScoped<IPatientService, PatientService>();

var app = builder.Build();

// The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
app.UseDeveloperExceptionPage();

app.UseRouting();

//using (var serviceScope = app.Services.CreateScope())
//{
//    var roleManager = serviceScope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();

//    if (!await roleManager.RoleExistsAsync("Admin"))
//    {
//        var adminRole = new IdentityRole("Admin");
//        await roleManager.CreateAsync(adminRole);
//    }

//    if (!await roleManager.RoleExistsAsync("User"))
//    {
//        var userRole = new IdentityRole("User");
//        await roleManager.CreateAsync(userRole);
//    }
//}

app.Configuration.Bind(nameof(jwtSettings), jwtSettings);

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
