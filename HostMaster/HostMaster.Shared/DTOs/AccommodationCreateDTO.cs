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
    public class AccommodationCreateDTO
    {
        public int Id { get; set; }

        [Display(Name = "AccommodationName", ResourceType = typeof(Literals))]
        [MaxLength(100, ErrorMessageResourceName = "MaxLength", ErrorMessageResourceType = typeof(Literals))]
        [Required(ErrorMessageResourceName = "RequiredField", ErrorMessageResourceType = typeof(Literals))]
        public string Name { get; set; } = null!;

        [Display(Name = "AccommodationAddress", ResourceType = typeof(Literals))]
        [MaxLength(50, ErrorMessageResourceName = "MaxLength", ErrorMessageResourceType = typeof(Literals))]
        [Required(ErrorMessageResourceName = "RequiredField", ErrorMessageResourceType = typeof(Literals))]
        public string Address { get; set; } = null!;

        [Display(Name = "AccommodationPhoneNumber", ResourceType = typeof(Literals))]
        [MaxLength(50, ErrorMessageResourceName = "MaxLength", ErrorMessageResourceType = typeof(Literals))]
        [Required(ErrorMessageResourceName = "RequiredField", ErrorMessageResourceType = typeof(Literals))]
        public string PhoneNumber { get; set; } = null!;

        // Foreign keys
        public int CityId { get; set; }

        public City? City { get; set; }
    }
}