using Polyclinic.Domain.Interfaces;
using Polyclinic.Domain.Models;
using Polyclinic.Domain.ViewModels;

namespace Polyclinic.Service.Interfaces;

public interface IPatientService : IService<Patient, PatientViewModel>
{
    Task<IBaseResponce<Patient>> GetByPhoneNumber(string phoneNimber);

    Task<IBaseResponce<Patient>> GetBySNILS(string snils);
}
