using System.ComponentModel.DataAnnotations;

namespace HostMaster.Shared.Entities;

public class RoomType
{
    public int Id { get; set; }

    [Required]
    public string TypeName { get; set; } = null!;

    [Required]
    public string Description { get; set; } = null!;

    // Relationships
    public ICollection<Room>? Rooms { get; set; }

    public static implicit operator RoomType(Room v)
    {
        throw new NotImplementedException();
    }
}