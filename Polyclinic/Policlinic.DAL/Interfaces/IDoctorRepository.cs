using Polyclinic.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Polyclinic.DAL.Interfaces;

public interface IDoctorRepository : IBaseRepository<Doctor>
{
    Task<Doctor> GetBySurname(string surname);

    Task<Doctor> GetBySpecialization(string spesialization);

}
