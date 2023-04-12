﻿namespace Polyclinic.Domain.ViewModels;
public class Patient
{
    public int Id { get; set; }

    public string Name { get; set; }

    public string Surname { get; set; }

    public string MiddleName { get; set; }

    public string PhoneNumber { get; set; }

    public string SNILS { get; set; }
}