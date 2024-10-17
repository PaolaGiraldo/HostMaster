using HostMaster.Shared.Resources;
using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.Localization;

namespace HostMaster.Frontend.Pages;

public partial class About
{
    [Inject] private IStringLocalizer<Literals> Localizer { get; set; } = null!;
}