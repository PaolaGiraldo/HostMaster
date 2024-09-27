using HostMaster.Shared.Resources;
using System.ComponentModel.DataAnnotations;

namespace HostMaster.Shared.Entities;

public class City
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

    // Relationships
    public ICollection<Accommodation>? Accommodations { get; set; }
}