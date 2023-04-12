using Polyclinic.Domain.Models;

namespace Polyclinic.DAL.Interfaces;

public interface IPatientRepository : IBaseRepository<Patient>
{
    Task<Doctor> GetByPhoneNumber(string phoneNumber);

    Task<Doctor> GetBySNILS(string snils);
}
