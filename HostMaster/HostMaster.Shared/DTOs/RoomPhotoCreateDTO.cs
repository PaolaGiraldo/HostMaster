using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HostMaster.Shared.DTOs
{
    internal class RoomPhotoCreateDTO
    {
        [Required]
        public string RoomPhotoName { get; set; } = null!;
    }
}