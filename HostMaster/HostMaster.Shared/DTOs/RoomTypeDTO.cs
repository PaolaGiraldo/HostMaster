using HostMaster.Shared.Entities;
using HostMaster.Shared.Resources;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HostMaster.Shared.DTOs;

public class RoomTypeDTO
{
    public int Id { get; set; }

    [Display(Name = "RoomTypeName", ResourceType = typeof(Literals))]
    [MaxLength(20, ErrorMessageResourceName = "MaxLength", ErrorMessageResourceType = typeof(Literals))]
    [Required(ErrorMessageResourceName = "RequiredField", ErrorMessageResourceType = typeof(Literals))]
    public string TypeName { get; set; } = null!;

    [Display(Name = "Description", ResourceType = typeof(Literals))]
    [MaxLength(100, ErrorMessageResourceName = "MaxLength", ErrorMessageResourceType = typeof(Literals))]
    [Required(ErrorMessageResourceName = "RequiredField", ErrorMessageResourceType = typeof(Literals))]
    public string Description { get; set; } = null!;

    [Display(Name = "Price", ResourceType = typeof(Literals))]    
    [Required(ErrorMessageResourceName = "RequiredField", ErrorMessageResourceType = typeof(Literals))]
    public decimal Price { get; set; }

    [Display(Name = "MaxGuests", ResourceType = typeof(Literals))]    
    [Required(ErrorMessageResourceName = "RequiredField", ErrorMessageResourceType = typeof(Literals))]
    public int MaxGuests { get; set; }
}