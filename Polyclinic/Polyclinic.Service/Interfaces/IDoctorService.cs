using Polyclinic.Domain.Interfaces;
using Polyclinic.Domain.Models;
using Polyclinic.Domain.ViewModels;

namespace Polyclinic.Service.Interfaces;

public interface IDoctorService : IService<Doctor, DoctorViewModel>
{
    Task<IBaseResponce<Doctor>> GetBySurame(string surname);

    Task<IBaseResponce<Doctor>> GetBySpecialization(string specialization);
}
