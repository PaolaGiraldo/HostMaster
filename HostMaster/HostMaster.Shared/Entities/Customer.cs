﻿using HostMaster.Shared.Resources;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HostMaster.Shared.Entities;

public class Customer
{
    public int Id { get; set; }

    [Display(Name = "FirstName", ResourceType = typeof(Literals))]
    [MaxLength(100, ErrorMessageResourceName = "MaxLength", ErrorMessageResourceType = typeof(Literals))]
    [Required(ErrorMessageResourceName = "RequiredField", ErrorMessageResourceType = typeof(Literals))]
    public string FirstName { get; set; } = null!;

    [Display(Name = "LastName", ResourceType = typeof(Literals))]
    [MaxLength(100, ErrorMessageResourceName = "MaxLength", ErrorMessageResourceType = typeof(Literals))]
    [Required(ErrorMessageResourceName = "RequiredField", ErrorMessageResourceType = typeof(Literals))]
    public string LastName { get; set; } = null!;

    [Display(Name = "DocumentType", ResourceType = typeof(Literals))]
    [MaxLength(50, ErrorMessageResourceName = "MaxLength", ErrorMessageResourceType = typeof(Literals))]
    [Required(ErrorMessageResourceName = "RequiredField", ErrorMessageResourceType = typeof(Literals))]
    public string DocumentType { get; set; } = null!;

    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.None)]
    [Display(Name = "Document", ResourceType = typeof(Literals))]
    [Required(ErrorMessageResourceName = "RequiredField", ErrorMessageResourceType = typeof(Literals))]
    public int DocumentNumber { get; set; }

    [Display(Name = "Email", ResourceType = typeof(Literals))]
    [MaxLength(150, ErrorMessageResourceName = "MaxLength", ErrorMessageResourceType = typeof(Literals))]
    [Required(ErrorMessageResourceName = "RequiredField", ErrorMessageResourceType = typeof(Literals))]
    public string Email { get; set; } = null!;

    [Display(Name = "PhoneNumber", ResourceType = typeof(Literals))]
    [MaxLength(20, ErrorMessageResourceName = "MaxLength", ErrorMessageResourceType = typeof(Literals))]
    [Required(ErrorMessageResourceName = "RequiredField", ErrorMessageResourceType = typeof(Literals))]
    public string PhoneNumber { get; set; } = null!;

    // Relationships
    public ICollection<Reservation>? Reservations { get; set; }
}