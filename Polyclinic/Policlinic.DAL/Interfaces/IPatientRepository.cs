using Polyclinic.Domain.Models;

namespace Polyclinic.DAL.Interfaces;

public interface IPatientRepository : IBaseRepository<Patient>
{
    Task<Patient> GetByPhoneNumber(string phoneNumber);

    Task<Patient> GetBySNILS(string snils);
}
