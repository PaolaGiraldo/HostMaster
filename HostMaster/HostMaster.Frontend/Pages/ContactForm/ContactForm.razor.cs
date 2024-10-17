using HostMaster.Frontend.Repositories;
using HostMaster.Shared.DTOs;
using HostMaster.Shared.Resources;
using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.Localization;

namespace HostMaster.Frontend.Pages.ContactForm;

public partial class ContactForm
{
    [Inject] private IStringLocalizer<Literals> Localizer { get; set; } = null!;
}