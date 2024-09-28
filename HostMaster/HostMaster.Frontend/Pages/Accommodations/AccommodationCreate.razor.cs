using HostMaster.Frontend.Repositories;
using HostMaster.Shared.DTOs;
using HostMaster.Shared.Resources;
using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.Localization;
using MudBlazor;

namespace HostMaster.Frontend.Pages.Accommodations
{
    public partial class AccommodationCreate
    {
        private AccommodationForm? accommodationForm;
        private AccommodationCreateDTO accommodationcreateDTO = new();

        [Inject] private IRepository Repository { get; set; } = null!;
        [Inject] private NavigationManager NavigationManager { get; set; } = null!;
        [Inject] private ISnackbar Snackbar { get; set; } = null!;
        [Inject] private IStringLocalizer<Literals> Localizer { get; set; } = null!;

        private async Task CreateAsync()
        {
            var responseHttp = await Repository.PostAsync("/api/Accommodations", accommodationcreateDTO);
            if (responseHttp.Error)
            {
                var message = await responseHttp.GetErrorMessageAsync();
                Snackbar.Add(Localizer[message!], Severity.Error);
                return;
            }

            Console.Write(responseHttp.HttpResponseMessage.ToString());

            Return();
            Snackbar.Add(Localizer["RecordCreatedOk"], Severity.Success);
        }

        private void Return()
        {
            accommodationForm!.FormPostedSuccessfully = true;
            NavigationManager.NavigateTo("/Accommodations");
        }
    }
}