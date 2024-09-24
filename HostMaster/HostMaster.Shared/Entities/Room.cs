using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HostMaster.Shared.Entities;

public class Room
{
    public int Id { get; set; }

    [Required]
    public string RoomNumber { get; set; } = null!;

    public bool IsAvailable { get; set; }

    // Foreign keys
    public int AccommodationId { get; set; }

    public Accommodation? Accommodation { get; set; }

    public int RoomTypeId { get; set; }

    public RoomType? RoomType { get; set; }

    // Relationships
    public ICollection<Reservation>? Reservations { get; set; }

    public ICollection<RoomInventoryItem>? RoomInventoryItems { get; set; }

    public ICollection<RoomPhoto>? Photos { get; set; }
}