using HostMaster.Shared.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HostMaster.Shared.DTOs
{
    public class CityCreateDTO
    {
        public int Id { get; set; }

        [Required]
        public string Name { get; set; } = null!;

        // Foreign keys
        public int StateId { get; set; }

        public State? State { get; set; }


    }
}
