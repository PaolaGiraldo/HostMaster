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

namespace HostMaster.Frontend.Pages.Reservations;

public partial class ReservationForm
{
    private EditContext editContext = null!;
    private Accommodation selectedAccommodation = new();
    private List<Accommodation>? accommodations;

    private Room selectedRoom = new();
    private List<Room>? rooms;

    protected override void OnInitialized()
    {
        editContext = new(ReservationDTO);
        ReservationDTO.StartDate = DateTime.Now;
        ReservationDTO.EndDate = DateTime.Today.AddDays(3);
    }

    protected override async Task OnInitializedAsync()
    {
        await LoadAccommodationsAsync();
        await LoadRoomsAsync();
    }

    [EditorRequired, Parameter] public ReservationDTO ReservationDTO { get; set; } = null!;
    [EditorRequired, Parameter] public EventCallback OnValidSubmit { get; set; }
    [EditorRequired, Parameter] public EventCallback ReturnAction { get; set; }
    public bool FormPostedSuccessfully { get; set; } = false;
    [Inject] private SweetAlertService SweetAlertService { get; set; } = null!;
    [Inject] private IStringLocalizer<Literals> Localizer { get; set; } = null!;
    [Inject] private IRepository Repository { get; set; } = null!;

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

    private async Task LoadAccommodationsAsync()
    {
        var responseHttp = await Repository.GetAsync<List<Accommodation>>("/api/accommodations");
        if (responseHttp.Error)
        {
            var message = await responseHttp.GetErrorMessageAsync();
            await SweetAlertService.FireAsync("Error", message, SweetAlertIcon.Error);
            return;
        }

        accommodations = responseHttp.Response;
    }

    private async Task<IEnumerable<Accommodation>> SearchAccommodation(string searchText, CancellationToken cancellationToken)
    {
        await Task.Delay(5);
        if (string.IsNullOrWhiteSpace(searchText))
        {
            return accommodations!;
        }

        return accommodations!
            .Where(x => x.Name.Contains(searchText, StringComparison.InvariantCultureIgnoreCase))
            .ToList();
    }

    private void AccommodationChanged(Accommodation accommodation)
    {
        selectedAccommodation = accommodation;
        ReservationDTO.AccommodationId = accommodation.Id;
    }

    private async Task LoadRoomsAsync()
    {
        var responseHttp = await Repository.GetAsync<List<Room>>("/api/rooms");
        if (responseHttp.Error)
        {
            var message = await responseHttp.GetErrorMessageAsync();
            await SweetAlertService.FireAsync("Error", message, SweetAlertIcon.Error);
            return;
        }

        rooms = responseHttp.Response;
    }

    private async Task<IEnumerable<Room>> SearchRoom(string searchText, CancellationToken cancellationToken)
    {
        await Task.Delay(5);
        if (string.IsNullOrWhiteSpace(searchText))
        {
            return rooms!;
        }

        return rooms!
            .Where(x => x.AccommodationId.Equals(selectedAccommodation))
            .Where(x => x.RoomNumber.Contains(searchText, StringComparison.InvariantCultureIgnoreCase))
            .ToList();
    }

    private void RoomChanged(Room room)
    {
        selectedRoom = room;
        ReservationDTO.RoomId = room.Id;
    }
}