using HostMaster.Frontend.Pages.Rooms;
using HostMaster.Frontend.Repositories;
using HostMaster.Shared.DTOs;
using HostMaster.Shared.Resources;
using HostMaster.Shared.Responses;
using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.Localization;
using MudBlazor;
using System.Text.Json;

namespace HostMaster.Frontend.Pages.RoomTypes;

public partial class RoomTypeCreate
{
    private RoomTypeForm? roomTypeForm;
    private RoomTypeDTO RoomTypeDTO = new();
    private List<RoomPhotoCreateDTO> roomPhotoCreateDTO = new();

    [Inject] private IRepository Repository { get; set; } = null!;
    [Inject] private NavigationManager NavigationManager { get; set; } = null!;
    [Inject] private ISnackbar Snackbar { get; set; } = null!;
    [Inject] private IStringLocalizer<Literals> Localizer { get; set; } = null!;

    private async Task CreateAsync()
    {
        var responseHttp = await Repository.PostAsync("/api/roomTypes", RoomTypeDTO);
        if (responseHttp.Error)
        {
            var message = await responseHttp.GetErrorMessageAsync();
            Snackbar.Add(Localizer[message!], Severity.Error);
            return;
        }       

        Return();
        Snackbar.Add(Localizer["RecordCreatedOk"], Severity.Success);
    }

    private void Return()
    {
        roomTypeForm!.FormPostedSuccessfully = true;
        NavigationManager.NavigateTo("/roomTypes");
    }
}