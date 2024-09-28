using HostMaster.Frontend.Pages.RoomTypes;
using HostMaster.Frontend.Repositories;
using HostMaster.Shared.DTOs;
using HostMaster.Shared.Entities;
using HostMaster.Shared.Resources;
using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.Localization;
using MudBlazor;

namespace HostMaster.Frontend.Pages.Accommodations;

public partial class AccommodationEdit
{
    private AccommodationCreateDTO? accommodationCreateDTO;

    private AccommodationForm? accommodationForm;

    [Inject] private NavigationManager NavigationManager { get; set; } = null!;

    [Inject] private IRepository Repository { get; set; } = null!;
    [Inject] private ISnackbar Snackbar { get; set; } = null!;
    [Inject] private IStringLocalizer<Literals> Localizer { get; set; } = null!;

    [Parameter] public int Id { get; set; }

    protected override async Task OnInitializedAsync()
    {
        var responseHttp = await Repository.GetAsync<Accommodation>($"api/Accommodations/{Id}");

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

            var Accommodation = responseHttp.Response;

            accommodationCreateDTO = new AccommodationCreateDTO()
            {
                Id = Accommodation!.Id,
                Name = Accommodation.Name,
                Address = Accommodation.Address,
                PhoneNumber = Accommodation.PhoneNumber,
                CityId = Accommodation.CityId
            };
        }
    }

    private async Task EditAsync()
    {
        var responseHttp = await Repository.PutAsync("api/Accommodations", accommodationCreateDTO);

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
        accommodationForm!.FormPostedSuccessfully = true;
        NavigationManager.NavigateTo("Accommodations");
    }
}