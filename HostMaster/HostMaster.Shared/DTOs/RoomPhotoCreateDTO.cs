using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HostMaster.Shared.DTOs;

public class RoomPhotoCreateDTO
{
    [Required]
    public string RoomPhotoURL { get; set; } = null!;

    [Required]
    public int RoomId { get; set; }
}