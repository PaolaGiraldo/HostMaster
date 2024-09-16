using System.ComponentModel.DataAnnotations;

namespace HostMaster.Shared.Entities;

public class RoomPhoto
{
    public int Id { get; set; }

    [Required]
    public string RoomPhotoName { get; set; } = null!;

    // Foreign keys

    [Required]
    public int RoomId { get; set; }

    public Room? Room { get; set; }
}