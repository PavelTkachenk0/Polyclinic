namespace Polyclinic.Domain.Models;
//описание модели доктора
public class Doctor
{
    public int Id { get; set; }

    public string Name { get; set; }

    public string Surname { get; set; }

    public string MiddleName { get; set; }

    public string Specialization { get; set; }

}
