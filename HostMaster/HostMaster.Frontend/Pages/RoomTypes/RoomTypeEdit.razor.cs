using HostMaster.Frontend.Pages.Rooms;
using HostMaster.Frontend.Repositories;
using HostMaster.Shared.DTOs;
using HostMaster.Shared.Entities;
using HostMaster.Shared.Resources;
using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.Localization;
using MudBlazor;
using System.Text.Json;

namespace HostMaster.Frontend.Pages.RoomTypes;

public partial class RoomTypeEdit
{
    private RoomTypeDTO? roomTypeDTO;

    private RoomTypeForm? roomTypeForm;

    [Inject] private NavigationManager NavigationManager { get; set; } = null!;

    [Inject] private IRepository Repository { get; set; } = null!;
    [Inject] private ISnackbar Snackbar { get; set; } = null!;
    [Inject] private IStringLocalizer<Literals> Localizer { get; set; } = null!;

    [Parameter] public int Id { get; set; }

    protected override async Task OnInitializedAsync()
    {
        var responseHttp = await Repository.GetAsync<RoomType>($"api/roomTypes/{Id}");

        if (responseHttp.Error)
        {
            if (responseHttp.HttpResponseMessage.StatusCode == System.Net.HttpStatusCode.NotFound)
            {
                NavigationManager.NavigateTo("roomTypes");
            }
            else
            {
                var messageError = await responseHttp.GetErrorMessageAsync();
                Snackbar.Add(messageError!, Severity.Error);
            }
        }
        else
        {
            var roomJson = await responseHttp.HttpResponseMessage.Content.ReadAsStringAsync();
            Console.WriteLine("Raw JSON Response:");
            Console.WriteLine(roomJson);

            var roomType = responseHttp.Response;

            roomTypeDTO = new RoomTypeDTO()
            {
                Id = roomType!.Id,
                TypeName = roomType.TypeName,
                Description = roomType.Description,
                Price = roomType.Price,
                MaxGuests = roomType.MaxGuests
            };
        }
    }

    private async Task EditAsync()
    {
        var responseHttp = await Repository.PutAsync("api/RoomTypes", roomTypeDTO);

        if (responseHttp.Error)
        {
            var mensajeError = await responseHttp.GetErrorMessageAsync();
            Snackbar.Add(Localizer[mensajeError!], Severity.Error);
            return;
        }

        Return();
        Snackbar.Add(Localizer["RecordSavedOk"], Severity.Success);
    }

    private void Return()
    {
        roomTypeForm!.FormPostedSuccessfully = true;
        NavigationManager.NavigateTo("roomTypes");
    }
}