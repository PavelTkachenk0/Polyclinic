namespace Polyclinic.Domain.Interfaces;

public interface IEntity
{
    public int Id { get; set; }

    public string Name { get; set; }

    public string Surname { get; set; }

    public string MiddleName { get; set; }

}
