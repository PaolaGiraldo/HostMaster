using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HostMaster.Shared.Entities;

public class Accommodation
{
    public int Id { get; set; }

    [Required]
    public string Name { get; set; } = null!;

    [Required]
    public string Address { get; set; } = null!;

    [Required]
    public string PhoneNumber { get; set; } = null!;

    // Foreign keys
    public int CityId { get; set; }

    public City? City { get; set; }

    // Relationships
    public ICollection<Room>? Rooms { get; set; }

    public ICollection<Employee>? Employees { get; set; }
    public ICollection<Reservation>? Reservations { get; set; }
}