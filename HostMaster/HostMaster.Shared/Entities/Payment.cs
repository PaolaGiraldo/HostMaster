using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HostMaster.Shared.Entities;

public class Payment
{
    public int Id { get; set; }
    public decimal Amount { get; set; }

    public decimal Taxes { get; set; }
    public DateTime PaymentDate { get; set; }

    [Required]
    public string PaymentMethod { get; set; } = null!;

    // Foreign keys
    public int ReservationId { get; set; }

    [Required]
    public Reservation Reservation { get; set; } = null!;
}