using Polyclinic.Domain.Interfaces;
using Polyclinic.Domain.Models;
using Polyclinic.Domain.ViewModels;

namespace Polyclinic.Service.Interfaces;

public interface IDoctorService : IService<Doctor, DoctorViewModel>
{
    Task<IBaseResponse<Doctor>> GetBySurame(string surname);

    Task<IBaseResponse<Doctor>> GetBySpecialization(string specialization);
}
