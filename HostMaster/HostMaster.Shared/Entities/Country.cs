using System.ComponentModel.DataAnnotations;

namespace HostMaster.Shared.Entities;

public class Country
{
    public int CountryId { get; set; }

    [Required]
    public string Name { get; set; } = null!;

    // Relationships
    public ICollection<State>? States { get; set; }
}