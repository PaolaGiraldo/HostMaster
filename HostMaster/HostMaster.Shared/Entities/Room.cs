using HostMaster.Shared.Resources;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HostMaster.Shared.Entities;

public class Room
{
    public int Id { get; set; }

    [Display(Name = "RoomNumber", ResourceType = typeof(Literals))]
    [MaxLength(3, ErrorMessageResourceName = "MaxLength", ErrorMessageResourceType = typeof(Literals))]
    [Required(ErrorMessageResourceName = "RequiredField", ErrorMessageResourceType = typeof(Literals))]
    public string RoomNumber { get; set; } = null!;

    public bool IsAvailable { get; set; }

    // Foreign keys
    // Foreign keys
    [Display(Name = "AccommodationId", ResourceType = typeof(Literals))]
    [Required(ErrorMessageResourceName = "RequiredField", ErrorMessageResourceType = typeof(Literals))]
    public int AccommodationId { get; set; }

    public Accommodation? Accommodation { get; set; }

    // Foreign keys
    [Display(Name = "RoomTypeId", ResourceType = typeof(Literals))]    
    [Required(ErrorMessageResourceName = "RequiredField", ErrorMessageResourceType = typeof(Literals))]
    public int RoomTypeId { get; set; }

    public RoomType? RoomType { get; set; }

    // Relationships
    public ICollection<Reservation>? Reservations { get; set; }

    public ICollection<RoomInventoryItem>? RoomInventoryItems { get; set; }

    public ICollection<RoomPhoto>? Photos { get; set; }
}