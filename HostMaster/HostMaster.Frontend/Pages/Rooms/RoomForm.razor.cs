using CurrieTechnologies.Razor.SweetAlert2;
using HostMaster.Frontend.Repositories;
using HostMaster.Shared.DTOs;
using HostMaster.Shared.Entities;
using HostMaster.Shared.Resources;
using Microsoft.AspNetCore.Components.Forms;
using Microsoft.AspNetCore.Components.Routing;
using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.Localization;
using System.Text.Json;

namespace HostMaster.Frontend.Pages.Rooms;

public partial class RoomForm
{
    private List<IBrowserFile> files = new List<IBrowserFile>();

    private EditContext editContext = null!;

    protected override async Task OnInitializedAsync()
    {
        await LoadRoomTypesAsync();
    }

    private RoomType selectedRoomType = new();
    private List<RoomType>? roomTypes;

    [Inject] private SweetAlertService SweetAlertService { get; set; } = null!;
    [Inject] private IStringLocalizer<Literals> Localizer { get; set; } = null!;
    [Inject] private IRepository Repository { get; set; } = null!;

    [EditorRequired, Parameter] public RoomCreateDTO RoomCreateDTO { get; set; } = null!;
    [Parameter] public List<RoomPhotoCreateDTO> RoomPhotoCreateDTO { get; set; } = new List<RoomPhotoCreateDTO>();

    [EditorRequired, Parameter] public EventCallback OnValidSubmit { get; set; }
    [EditorRequired, Parameter] public EventCallback ReturnAction { get; set; }

    public bool FormPostedSuccessfully { get; set; } = false;

    protected override void OnInitialized()
    {
        editContext = new(RoomCreateDTO);
    }

    protected override void OnParametersSet()
    {
        base.OnParametersSet();
    }

    private void OnImagesSelected(List<string> imagesBase64)

    {
        //Console.WriteLine("OnImagesSelected");

        if (imagesBase64 != null && imagesBase64.Count > 0)
        {
            foreach (var base64Image in imagesBase64)
            {
                var roomPhoto = new RoomPhotoCreateDTO
                {
                    RoomPhotoURL = base64Image,
                    RoomId = RoomCreateDTO.Id,
                };

                RoomPhotoCreateDTO.Add(roomPhoto);
            }
        }
    }

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

    private async Task OnBeforeInternalNavigation(LocationChangingContext context)
    {
        // Console.Write("CHANGESSSSS");
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