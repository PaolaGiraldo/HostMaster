using System.ComponentModel.DataAnnotations;

namespace HostMaster.Shared.Entities;

public class RoomInventoryItem
{
    public int Id { get; set; }

    [Required]
    public string ItemName { get; set; } = null!;

    [Required]
    public int Quantity { get; set; }

    [Required]
    public string Condition { get; set; } = null!;

    // Foreign key
    public int RoomId { get; set; }

    public Room? Room { get; set; }
}