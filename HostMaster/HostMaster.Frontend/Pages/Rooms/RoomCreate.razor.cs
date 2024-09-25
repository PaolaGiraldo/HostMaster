using HostMaster.Frontend.Repositories;
using HostMaster.Shared.DTOs;
using HostMaster.Shared.Entities;
using HostMaster.Shared.Resources;
using HostMaster.Shared.Responses;
using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.Localization;
using MudBlazor;
using System.Text.Json;

namespace HostMaster.Frontend.Pages.Rooms;

public partial class RoomCreate
{
    private RoomForm? roomForm;
    private RoomCreateDTO roomCreateDTO = new();
    private List<RoomPhotoCreateDTO> roomPhotoCreateDTO = new();

    [Inject] private IRepository Repository { get; set; } = null!;
    [Inject] private NavigationManager NavigationManager { get; set; } = null!;
    [Inject] private ISnackbar Snackbar { get; set; } = null!;
    [Inject] private IStringLocalizer<Literals> Localizer { get; set; } = null!;

    private async Task CreateAsync()
    {
        var responseHttp = await Repository.PostAsync("/api/rooms", roomCreateDTO);
        if (responseHttp.Error)
        {
            var message = await responseHttp.GetErrorMessageAsync();
            Snackbar.Add(Localizer[message!], Severity.Error);
            return;
        }

        var jsonResponse = await responseHttp.HttpResponseMessage.Content.ReadAsStringAsync();

        // Console.WriteLine("ID DEL RESPONSE:");
        //  Console.WriteLine(jsonResponse);

        /* var options = new JsonSerializerOptions
         {
             PropertyNameCaseInsensitive = true // Ignorar mayúsculas y minúsculas
         };*/

        var createdRoom = JsonSerializer.Deserialize<RoomCreateResponse>(jsonResponse);

        var roomId = createdRoom!.id;

        // Console.WriteLine($"este es el ID: {createdRoom?.id}");

        //Console.WriteLine("CREATE ROOM");
        //Console.WriteLine(JsonSerializer.Serialize(roomCreateDTO, new JsonSerializerOptions { WriteIndented = true }));

        foreach (var roomPhotoDTO in roomPhotoCreateDTO)

        {
            roomPhotoDTO.RoomId = roomId;
            var responseHttp2 = await Repository.PostAsync("api/roomphotos/full", roomPhotoDTO);
            if (responseHttp2.Error)
            {
                var mensajeError = await responseHttp2.GetErrorMessageAsync();
                Snackbar.Add(Localizer[mensajeError!], Severity.Error);
                return;
            }
        }

        Return();
        Snackbar.Add(Localizer["RecordCreatedOk"], Severity.Success);
    }

    private void Return()
    {
        roomForm!.FormPostedSuccessfully = true;
        NavigationManager.NavigateTo("/rooms");
    }
}