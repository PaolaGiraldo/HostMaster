using System.ComponentModel.DataAnnotations;

namespace HostMaster.Shared.Entities;

public class Customer
{
    public int Id { get; set; }

    [Required]
    public string FirstName { get; set; } = null!;

    [Required]
    public string LastName { get; set; } = null!;

    [Required]
    public string DocumentType { get; set; } = null!;

    [Required]
    public int DocumentNumber { get; set; }

    [Required]
    public string Email { get; set; } = null!;

    [Required]
    public string PhoneNumber { get; set; } = null!;

    // Relationships
    public ICollection<Reservation>? Reservations { get; set; }
}