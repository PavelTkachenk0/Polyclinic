﻿using Microsoft.EntityFrameworkCore;
using Polyclinic.DAL.Interfaces;
using Polyclinic.Domain.Models;

namespace Polyclinic.DAL.Repositories;

public class DoctorRepository : IDoctorRepository
{
    private readonly ApplicationDbContext _db; 

    public DoctorRepository(ApplicationDbContext db)
    {
        _db = db;
    }
    public async Task<bool> Create(Doctor entity)
    {
        await _db.Doctor.AddAsync(entity);
        await _db.SaveChangesAsync();
        return true;
    }

    public async Task<bool> Delete(Doctor entity)
    {
        _db.Doctor.Remove(entity);
        await _db.SaveChangesAsync();
        return true;
    }

    public async Task<Doctor> Update(Doctor entity)
    {
        _db.Doctor.Update(entity);
        await _db.SaveChangesAsync();

        return entity;
    }

    public async Task<Doctor> GetById(int id)
    {
        return await _db.Doctor.FirstOrDefaultAsync(x => x.Id == id);
    }

    public async Task<List<Doctor>> GetAll() 
    {
        return await _db.Doctor.ToListAsync();
    }

    public async Task<Doctor> GetBySurname(string surname)
    {
        return await _db.Doctor.FirstOrDefaultAsync(x => x.Surname == surname);
    }

    public async Task<Doctor> GetBySpecialization(string spesialization)
    {
        return await _db.Doctor.FirstOrDefaultAsync(x => x.Specialization == spesialization);
    }
}