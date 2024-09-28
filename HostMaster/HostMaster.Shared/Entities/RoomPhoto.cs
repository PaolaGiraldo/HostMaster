using HostMaster.Shared.Resources;
using System.ComponentModel.DataAnnotations;

namespace HostMaster.Shared.Entities;

public class RoomPhoto
{
    public int Id { get; set; }

    [Display(Name = "RoomPhotoURL", ResourceType = typeof(Literals))]    
    [Required(ErrorMessageResourceName = "RequiredField", ErrorMessageResourceType = typeof(Literals))]
    public string RoomPhotoURL { get; set; } = null!;

    // Foreign keys

    [Display(Name = "RoomId", ResourceType = typeof(Literals))]    
    [Required(ErrorMessageResourceName = "RequiredField", ErrorMessageResourceType = typeof(Literals))]
    public int RoomId { get; set; }

    public Room? Room { get; set; }
}