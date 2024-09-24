using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HostMaster.Shared.Entities;

public class Reservation
{
    public int Id { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
    public int NumberOfGuests { get; set; }

    [Required]
    public string State { get; set; } = null!;

    //Foreign keys
    public int RoomId { get; set; }

    [Required]
    public Room Room { get; set; } = null!;

    public int CustomerId { get; set; }

    [Required]
    public Customer Customer { get; set; } = null!;

    public int AccommodationId { get; set; }

    [Required]
    public Accommodation Accommodation { get; set; } = null!;

    // Relationships
    public ICollection<Payment>? Payments { get; set; }

    public ICollection<ExtraService>? ExtraServices { get; set; }

    public ICollection<Room>? Rooms { get; set; }
}