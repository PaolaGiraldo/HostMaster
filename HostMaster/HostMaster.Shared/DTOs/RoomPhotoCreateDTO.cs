using HostMaster.Shared.Resources;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HostMaster.Shared.DTOs;

public class RoomPhotoCreateDTO
{
    [Display(Name = "RoomPhotoURL", ResourceType = typeof(Literals))]
    [MaxLength(200, ErrorMessageResourceName = "MaxLength", ErrorMessageResourceType = typeof(Literals))]
    [Required(ErrorMessageResourceName = "RequiredField", ErrorMessageResourceType = typeof(Literals))]
    public string RoomPhotoURL { get; set; } = null!;

    // Foreign keys

    [Display(Name = "RoomId", ResourceType = typeof(Literals))]
    [MaxLength(10, ErrorMessageResourceName = "MaxLength", ErrorMessageResourceType = typeof(Literals))]
    [Required(ErrorMessageResourceName = "RequiredField", ErrorMessageResourceType = typeof(Literals))]
    public int RoomId { get; set; }
}