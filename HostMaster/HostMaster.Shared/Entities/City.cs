using System.ComponentModel.DataAnnotations;

namespace HostMaster.Shared.Entities;

public class City
{
    public int Id { get; set; }

    [Required]
    public string Name { get; set; } = null!;

    // Foreign keys
    public int StateId { get; set; }

    public State? State { get; set; }

    // Relationships
    public ICollection<Accommodation>? Accommodations { get; set; }
}