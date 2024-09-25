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
    public int Id { get; set; }

    [Required]
    public string TypeName { get; set; } = null!;

    [Required]
    public string Description { get; set; } = null!;

    public decimal Price { get; set; }

    public int MaxGuests { get; set; }
}