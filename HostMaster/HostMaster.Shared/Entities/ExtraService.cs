using System.ComponentModel.DataAnnotations;

namespace HostMaster.Shared.Entities;

public class ExtraService
{
    public int Id { get; set; }

    [Required]
    public string ServiceName { get; set; } = null!;

    public decimal Price { get; set; }

    // Relationships
    public ICollection<Reservation>? Reservations { get; set; }
}