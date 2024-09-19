using CurrieTechnologies.Razor.SweetAlert2;
using HostMaster.Frontend.Repositories;
using HostMaster.Shared.DTOs;
using HostMaster.Shared.Entities;
using HostMaster.Shared.Resources;
using Microsoft.AspNetCore.Components.Forms;
using Microsoft.AspNetCore.Components.Routing;
using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.Localization;

namespace HostMaster.Frontend.Pages.Rooms;

public partial class RoomForm
{
    private EditContext editContext = null!;
    private RoomType selectedRoomType = new();
    private List<RoomType>? roomTypes;
    private string? imageUrl;

    [Inject] private SweetAlertService SweetAlertService { get; set; } = null!;
    [Inject] private IStringLocalizer<Literals> Localizer { get; set; } = null!;
    [Inject] private IRepository Repository { get; set; } = null!;

    [EditorRequired, Parameter] public RoomCreateDTO RoomCreateDTO { get; set; } = null!;
    [EditorRequired, Parameter] public EventCallback OnValidSubmit { get; set; }
    [EditorRequired, Parameter] public EventCallback ReturnAction { get; set; }

    public bool FormPostedSuccessfully { get; set; } = false;

    protected override void OnInitialized()
    {
        editContext = new(RoomCreateDTO);
    }

    protected override async Task OnInitializedAsync()
    {
        await LoadRoomTypesAsync();
    }

    /* protected override void OnParametersSet()
     {
         base.OnParametersSet();
         if (!string.IsNullOrEmpty(RoomCreateDTO.Image))
         {
             imageUrl = RoomCreateDTO.Image;
             TeamDTO.Image = null;
         }
     }*/

    private async Task LoadRoomTypesAsync()
    {
        var responseHttp = await Repository.GetAsync<List<RoomType>>("/api/RoomTypes");
        if (responseHttp.Error)
        {
            var message = await responseHttp.GetErrorMessageAsync();
            await SweetAlertService.FireAsync("Error", message, SweetAlertIcon.Error);
            return;
        }

        roomTypes = responseHttp.Response;
    }

    /*private void ImageSelected(string imagenBase64)
    {
        RoomCreateDTO.Image = imagenBase64;
        imageUrl = null;
    }*/

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

    private async Task<IEnumerable<RoomType>> SearchRoomType(string searchText, CancellationToken cancellationToken)
    {
        await Task.Delay(5);
        if (string.IsNullOrWhiteSpace(searchText))
        {
            return roomTypes!;
        }

        return roomTypes!
            .Where(x => x.TypeName.Contains(searchText, StringComparison.InvariantCultureIgnoreCase))
            .ToList();
    }

    private void RoomTypeChanged(RoomType roomType)
    {
        selectedRoomType = roomType;
        RoomCreateDTO.RoomTypeId = roomType.Id;
    }
}