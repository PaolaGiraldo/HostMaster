using HostMaster.Shared.Entities;
using HostMaster.Shared.Resources;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HostMaster.Shared.DTOs;

public class ReservationDTO
{
    public int Id { get; set; }

    [Display(Name = "StartDate", ResourceType = typeof(Literals))]
    [Required(ErrorMessageResourceName = "RequiredField", ErrorMessageResourceType = typeof(Literals))]
    [StartDateLessThan("EndDate", ErrorMessageResourceName = "StartDateLess", ErrorMessageResourceType = typeof(Literals))]
    public DateTime? StartDate { get; set; }

    [Display(Name = "EndDate", ResourceType = typeof(Literals))]
    [Required(ErrorMessageResourceName = "RequiredField", ErrorMessageResourceType = typeof(Literals))]
    [CompareDate("StartDate", ErrorMessageResourceName = "EndDateGreater", ErrorMessageResourceType = typeof(Literals))]
    public DateTime? EndDate { get; set; }

    [Display(Name = "NumberOfGuests", ResourceType = typeof(Literals))]
    [Required(ErrorMessageResourceName = "RequiredField", ErrorMessageResourceType = typeof(Literals))]
    public int NumberOfGuests { get; set; }

    [Display(Name = "ReservationState", ResourceType = typeof(Literals))]
    public string ReservationState { get; set; } = null!;

    //Foreign keys
    [Display(Name = "RoomId", ResourceType = typeof(Literals))]
    [Required(ErrorMessageResourceName = "RequiredField", ErrorMessageResourceType = typeof(Literals))]
    public int RoomId { get; set; }

    [Display(Name = "AccommodationId", ResourceType = typeof(Literals))]
    [Required(ErrorMessageResourceName = "RequiredField", ErrorMessageResourceType = typeof(Literals))]
    public int AccommodationId { get; set; }

    [Display(Name = "CustomerDocument", ResourceType = typeof(Literals))]
    [Required(ErrorMessageResourceName = "RequiredField", ErrorMessageResourceType = typeof(Literals))]
    public int CustomerDocument { get; set; }
}

public class CompareDateAttribute : ValidationAttribute
{
    private readonly string _comparisonProperty;

    public CompareDateAttribute(string comparisonProperty)
    {
        _comparisonProperty = comparisonProperty;
    }

    protected override ValidationResult IsValid(object value, ValidationContext validationContext)
    {
        var comparisonValue = validationContext
            .ObjectType
            .GetProperty(_comparisonProperty)
            .GetValue(validationContext.ObjectInstance);

        if (value is DateTime endDate && comparisonValue is DateTime startDate)
        {
            if (endDate <= startDate)
            {
                return new ValidationResult(ErrorMessage);
            }
        }

        return ValidationResult.Success!;
    }
}

public class StartDateLessThanAttribute : ValidationAttribute
{
    private readonly string _comparisonProperty;

    public StartDateLessThanAttribute(string comparisonProperty)
    {
        _comparisonProperty = comparisonProperty;
    }

    protected override ValidationResult IsValid(object value, ValidationContext validationContext)
    {
        var startDate = value as DateTime?;
        var endDate = validationContext
            .ObjectType
            .GetProperty(_comparisonProperty)
            .GetValue(validationContext.ObjectInstance) as DateTime?;

        if (startDate.HasValue && endDate.HasValue)
        {
            if (startDate >= endDate)
            {
                return new ValidationResult(ErrorMessage);
            }
        }

        return ValidationResult.Success;
    }
}