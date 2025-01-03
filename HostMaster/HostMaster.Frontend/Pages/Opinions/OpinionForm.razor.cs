using CurrieTechnologies.Razor.SweetAlert2;
using HostMaster.Frontend.Repositories;
using HostMaster.Shared.DTOs;
using HostMaster.Shared.Entities;
using HostMaster.Shared.Resources;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;
using Microsoft.AspNetCore.Components.Routing;
using Microsoft.Extensions.Localization;
using MudBlazor;

namespace HostMaster.Frontend.Pages.Opinions;

public partial class OpinionForm
{
    private EditContext editContext = null!;
    private MudForm opinionForm;

    protected override void OnInitialized()
    {
        editContext = new(OpinionDTO);
        OpinionDTO.CreatedDate = DateTime.Now;
    }

    [EditorRequired, Parameter] public OpinionDTO OpinionDTO { get; set; } = null!;
    [EditorRequired, Parameter] public EventCallback OnValidSubmit { get; set; }
    [EditorRequired, Parameter] public EventCallback ReturnAction { get; set; }
    public bool FormPostedSuccessfully { get; set; } = false;
    [Inject] private SweetAlertService SweetAlertService { get; set; } = null!;
    [Inject] private IStringLocalizer<Literals> Localizer { get; set; } = null!;
    [Inject] private IRepository Repository { get; set; } = null!;

    private int selectedVal = 0;
    private int? activeVal;

    private void HandleHoveredValueChanged(int? val) => activeVal = val;

    private string LabelText => (activeVal ?? selectedVal) switch
    {
        1 => Localizer["VeryBad"],
        2 => Localizer["Bad"],
        3 => Localizer["Sufficient"],
        4 => Localizer["Good"],
        5 => Localizer["Awesome"],
        _ => Localizer["RateYourExperience"]
    };

    protected override void OnParametersSet()
    {
        base.OnParametersSet();
    }

    private async Task OnBeforeInternalNavigation(LocationChangingContext context)
    {
        var formWasEdited = editContext.IsModified();
        if (!formWasEdited || FormPostedSuccessfully)
        {
            return;
        }
        var result = await SweetAlertService.FireAsync(new SweetAlertOptions
        {
            Title = Localizer["Confirmation"],
            Text = Localizer["LeaveAndLoseChanges"],
            Icon = SweetAlertIcon.Warning,
            ShowCancelButton = true,
            CancelButtonText = Localizer["Cancel"],
        });
        var confirm = !string.IsNullOrEmpty(result.Value);
        if (confirm)
        {
            return;
        }
        context.PreventNavigation();
    }

    private void ClearForm()
    {
        // Limpiar el modelo y restablecer el estado del formulario
        OpinionDTO = new OpinionDTO(); // Restablecer el modelo a su estado inicial
        StateHasChanged(); // Actualizar la interfaz de usuario
    }
}