using HostMaster.Shared.Entities;
using HostMaster.Shared.Resources;
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

        [Display(Name = "CityName", ResourceType = typeof(Literals))]
        [MaxLength(50, ErrorMessageResourceName = "MaxLength", ErrorMessageResourceType = typeof(Literals))]
        [Required(ErrorMessageResourceName = "RequiredField", ErrorMessageResourceType = typeof(Literals))]
        public string Name { get; set; } = null!;

        // Foreign keys
        [Display(Name = "State", ResourceType = typeof(Literals))]        
        [Required(ErrorMessageResourceName = "RequiredField", ErrorMessageResourceType = typeof(Literals))]
        public int StateId { get; set; }

        public State? State { get; set; }
    }
}