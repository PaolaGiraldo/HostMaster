using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HostMaster.Shared.Entities;

public class Room
{
    public int RoomId { get; set; }

    [Required]
    public string RoomNumber { get; set; } = null!;

    public decimal Price { get; set; }
    public bool IsAvailable { get; set; }

    // Foreign keys
    public int AccommodationId { get; set; }

    [Required]
    public Accommodation Accommodation { get; set; } = null!;

    public int RoomTypeId { get; set; }

    [Required]
    public RoomType RoomType { get; set; } = null!;

    // Relationships
    public ICollection<Reservation>? Reservations { get; set; }

    public ICollection<RoomInventoryItem>? RoomInventoryItems { get; set; }

    public ICollection<RoomPhoto>? Photos { get; set; }
}