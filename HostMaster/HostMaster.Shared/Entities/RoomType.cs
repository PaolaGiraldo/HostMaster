using System.ComponentModel.DataAnnotations;

namespace HostMaster.Shared.Entities;

public class RoomType
{
    public int Id { get; set; }

    [Required]
    public string TypeName { get; set; } = null!;

    [Required]
    public string Description { get; set; } = null!;

    public decimal Price { get; set; }

    public int MaxGuests { get; set; }

    // Relationships
    public ICollection<Room>? Rooms { get; set; }
}