using System.ComponentModel.DataAnnotations;

namespace HostMaster.Shared.Entities;

public class RoomType
{
    public int RoomTypeId { get; set; }

    [Required]
    public string TypeName { get; set; } = null!;

    [Required]
    public string Description { get; set; } = null!;

    // Relationships
    public ICollection<Room>? Rooms { get; set; }
}