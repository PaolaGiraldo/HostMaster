using HostMaster.Frontend.Repositories;
using HostMaster.Shared.DTOs;
using HostMaster.Shared.Entities;
using HostMaster.Shared.Resources;
using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.Localization;
using MudBlazor;
using System.Text.Json;
using static MudBlazor.Colors;

namespace HostMaster.Frontend.Pages.Rooms;

public partial class RoomEdit
{
    private RoomCreateDTO? roomCreateDTO;
    private List<RoomPhotoCreateDTO> roomPhotoCreateDTO { get; set; } = [];

    private RoomForm? roomForm;
    private RoomType selectedRoomType = new();

    [Inject] private NavigationManager NavigationManager { get; set; } = null!;

    [Inject] private IRepository Repository { get; set; } = null!;
    [Inject] private ISnackbar Snackbar { get; set; } = null!;
    [Inject] private IStringLocalizer<Literals> Localizer { get; set; } = null!;

    [Parameter] public int Id { get; set; }

    protected override async Task OnInitializedAsync()
    {
        var responseHttp = await Repository.GetAsync<Room>($"api/rooms/{Id}");

        if (responseHttp.Error)
        {
            if (responseHttp.HttpResponseMessage.StatusCode == System.Net.HttpStatusCode.NotFound)
            {
                NavigationManager.NavigateTo("rooms");
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
            var room = responseHttp.Response;
            roomCreateDTO = new RoomCreateDTO()
            {
                Id = room!.Id,
                RoomNumber = room!.RoomNumber,
                IsAvailable = room.IsAvailable,
                AccommodationId = room.AccommodationId
            };
            selectedRoomType = room.RoomType!;
        }
    }

    private async Task EditAsync()
    {
        // Console.WriteLine("EDIT ROOM");
        // Console.WriteLine(JsonSerializer.Serialize(roomCreateDTO, new JsonSerializerOptions { WriteIndented = true }));

        // Console.WriteLine("ANTES DEL PUT");
        // Console.WriteLine(JsonSerializer.Serialize(roomCreateDTO, new JsonSerializerOptions { WriteIndented = true }));
        var responseHttp = await Repository.PutAsync("api/rooms", roomCreateDTO);

        if (responseHttp.Error)
        {
            var mensajeError = await responseHttp.GetErrorMessageAsync();
            Snackbar.Add(Localizer[mensajeError!], Severity.Error);
            return;
        }

        Console.WriteLine("PHOTOSSSSS EN EDIT");
        Console.WriteLine(JsonSerializer.Serialize(roomPhotoCreateDTO, new JsonSerializerOptions { WriteIndented = true }));
        if (roomPhotoCreateDTO.Count != 0)
        {
            Console.WriteLine("DELETE PHOTOS");
            var responseHttp3 = await Repository.DeleteAsync($"api/roomphotos/by-roomId/{roomCreateDTO?.Id}");
            if (responseHttp3.Error)
            {
                var mensajeError = await responseHttp3.GetErrorMessageAsync();
                Snackbar.Add(Localizer[mensajeError!], Severity.Error);
                return;
            }
        }
        foreach (var roomPhotoDTO in roomPhotoCreateDTO)

        {
            Console.WriteLine("EDIT PHOTO ROOM");
            Console.WriteLine(JsonSerializer.Serialize(roomPhotoDTO, new JsonSerializerOptions { WriteIndented = true }));

            var responseHttp2 = await Repository.PostAsync("api/roomphotos/full", roomPhotoDTO);
            if (responseHttp2.Error)
            {
                var mensajeError = await responseHttp2.GetErrorMessageAsync();
                Snackbar.Add(Localizer[mensajeError!], Severity.Error);
                return;
            }
        }
        Return();
        Snackbar.Add(Localizer["RecordSavedOk"], Severity.Success);
    }

    private void Return()
    {
        roomForm!.FormPostedSuccessfully = true;
        NavigationManager.NavigateTo("rooms");
    }
}