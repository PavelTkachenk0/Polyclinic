using Polyclinic.Domain.Interfaces;
using Polyclinic.Domain.Models;
using Polyclinic.Domain.ViewModels;

namespace Polyclinic.Service.Interfaces;

public interface IPatientService : IService<Patient, PatientViewModel>
{
    Task<IBaseResponse<Patient>> GetByPhoneNumber(string phoneNimber);

    Task<IBaseResponse<Patient>> GetBySNILS(string snils);
}
