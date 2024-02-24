namespace Polyclinic.Domain.Models;
//описание модели услуг
public class Amenitie
{
    public int Id { get; set; }

    public string Name { get; set; }

    public string Description { get; set; }

    public DateTime StartOfReception { get; set; }

    public DateTime EndOfReception { get; set; }

}