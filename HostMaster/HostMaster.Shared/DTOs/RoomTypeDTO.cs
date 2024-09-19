using HostMaster.Shared.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HostMaster.Shared.DTOs;

public class RoomTypeDTO
{
    [Required]
    public string TypeName { get; set; } = null!;

    [Required]
    public string Description { get; set; } = null!;
}