using System.ComponentModel.DataAnnotations;

namespace HostMaster.Shared.Entities;

public class State
{
    public int StateId { get; set; }

    [Required]
    public string Name { get; set; } = null!;

    // Foreign keys
    public int CountryId { get; set; }

    [Required]
    public Country Country { get; set; } = null!;

    // Relationships
    public ICollection<City>? Cities { get; set; }
}