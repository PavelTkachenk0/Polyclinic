using Microsoft.EntityFrameworkCore;
using Polyclinic.DAL.Interfaces;
using Polyclinic.Domain.Models;

namespace Polyclinic.DAL.Repositories
{
    public class PatientRepository : IPatientRepository
    {

        private readonly ApplicationDbContext _db;

        public PatientRepository(ApplicationDbContext db)
        {
            _db = db;
        }
        public async Task<bool> Create(Patient entity)
        {
            await _db.Patient.AddAsync(entity);
            await _db.SaveChangesAsync();
            return true;
        }

        public async Task<bool> Delete(Patient entity)
        {
            _db.Patient.Remove(entity);
            await _db.SaveChangesAsync();
            return true;
        }

        public async Task<Patient> GetById(int id)
        {
            return await _db.Patient.FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<List<Patient>> GetAll()
        {
            return await _db.Patient.ToListAsync();
        }

        public async Task<Patient> GetByPhoneNumber(string phoneNumber)
        {
            return await _db.Patient.FirstOrDefaultAsync(x => x.PhoneNumber == phoneNumber);
        }

        public async Task<Patient> GetBySNILS(string snils)
        {
            return await _db.Patient.FirstOrDefaultAsync(x => x.SNILS == snils);
        }
    }
}
