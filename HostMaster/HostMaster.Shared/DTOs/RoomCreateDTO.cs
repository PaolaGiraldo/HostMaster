using HostMaster.Shared.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HostMaster.Shared.DTOs;

public class RoomCreateDTO
{
    public int Id { get; set; }

    [Required]
    public string RoomNumber { get; set; } = null!;

    public decimal Price { get; set; }
    public bool IsAvailable { get; set; }

    // Foreign keys
    public int AccommodationId { get; set; }

    [Required]
    public Accommodation Accommodation { get; set; } = null!;

    public int RoomTypeId { get; set; }

    [Required]
    public RoomType RoomType { get; set; } = null!;

    // Relationships
    public ICollection<RoomPhoto>? Photos { get; set; }
}