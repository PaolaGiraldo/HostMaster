using Blazored.Modal.Services;
using Blazored.Modal;
using CurrieTechnologies.Razor.SweetAlert2;
using HostMaster.Frontend.Repositories;
using HostMaster.Frontend.Shared;
using HostMaster.Shared.Entities;
using HostMaster.Shared.Resources;
using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.Localization;
using MudBlazor;
using System.Net;

namespace HostMaster.Frontend.Pages.Countries;

public partial class CountriesIndex
{
    private int currentPage = 1;
    private int totalPages;

    [Inject] private IRepository Repository { get; set; } = null!;
    [Inject] private SweetAlertService SweetAlertService { get; set; } = null!;
    [Inject] private NavigationManager NavigationManager { get; set; } = null!;

    [Inject] private IStringLocalizer<Literals> Localizer { get; set; } = null!;

    [Parameter, SupplyParameterFromQuery] public string Page { get; set; } = string.Empty;
    [Parameter, SupplyParameterFromQuery] public string Filter { get; set; } = string.Empty;
    [Parameter, SupplyParameterFromQuery] public int RecordsNumber { get; set; } = 2;
    [CascadingParameter] private IModalService Modal { get; set; } = default!;

    public List<Country>? Countries { get; set; }

    protected override async Task OnInitializedAsync()
    {
        await LoadAsync();
    }

    private async Task ShowModalAsync(int id = 0, bool isEdit = false)
    {
        IModalReference modalReference;

        if (isEdit)
        {
            modalReference = Modal.Show<CountryEdit>(string.Empty, new ModalParameters().Add("Id", id));
        }
        else
        {
            modalReference = Modal.Show<CountryCreate>();
        }

        var result = await modalReference.Result;
        if (result.Confirmed)
        {
            await LoadAsync();
        }
    }

    private async Task SelectedRecordsNumberAsync(int recordsnumber)
    {
        RecordsNumber = recordsnumber;
        int page = 1;
        await LoadAsync(page);
        await SelectedPageAsync(page);
    }

    private async Task FilterCallBack(string filter)
    {
        Filter = filter;
        await ApplyFilterAsync();
        StateHasChanged();
    }

    private async Task SelectedPageAsync(int page)
    {
        currentPage = page;
        await LoadAsync(page);
    }

    private async Task LoadAsync(int page = 1)
    {
        if (!string.IsNullOrWhiteSpace(Page))
        {
            page = Convert.ToInt32(Page);
        }

        var ok = await LoadListAsync(page);
        if (ok)
        {
            await LoadPagesAsync();
        }
    }

    private void ValidateRecordsNumber()
    {
        if (RecordsNumber == 0)
        {
            RecordsNumber = 2;
        }
    }

    private async Task<bool> LoadListAsync(int page)
    {
        ValidateRecordsNumber();
        var url = $"api/countries?page={page}&recordsnumber={RecordsNumber}";
        if (!string.IsNullOrEmpty(Filter))
        {
            url += $"&filter={Filter}";
        }

        var responseHttp = await Repository.GetAsync<List<Country>>(url);
        if (responseHttp.Error)
        {
            var message = await responseHttp.GetErrorMessageAsync();
            await SweetAlertService.FireAsync("Error", message, SweetAlertIcon.Error);
            return false;
        }
        Countries = responseHttp.Response;
        return true;
    }

    private async Task LoadPagesAsync()
    {
        var url = $"api/countries/totalPages?recordsnumber={RecordsNumber}";
        if (!string.IsNullOrEmpty(Filter))
        {
            url += $"&filter={Filter}";
        }

        var responseHttp = await Repository.GetAsync<int>(url);
        if (responseHttp.Error)
        {
            var message = await responseHttp.GetErrorMessageAsync();
            await SweetAlertService.FireAsync("Error", message, SweetAlertIcon.Error);
            return;
        }
        totalPages = responseHttp.Response;
    }

    private async Task ApplyFilterAsync()
    {
        int page = 1;
        await LoadAsync(page);
        await SelectedPageAsync(page);
    }

    private async Task DeleteAsycn(Country country)
    {
        var result = await SweetAlertService.FireAsync(new SweetAlertOptions
        {
            Title = "Confirmaci�n",
            Text = $"�Estas seguro de querer borrar el pa�s: {country.Name}?",
            Icon = SweetAlertIcon.Question,
            ShowCancelButton = true,
        });
        var confirm = string.IsNullOrEmpty(result.Value);
        if (confirm)
        {
            return;
        }

        var responseHttp = await Repository.DeleteAsync($"api/countries/{country.Id}");
        if (responseHttp.Error)
        {
            if (responseHttp.HttpResponseMessage.StatusCode == HttpStatusCode.NotFound)
            {
                NavigationManager.NavigateTo("/countries");
            }
            else
            {
                var mensajeError = await responseHttp.GetErrorMessageAsync();
                await SweetAlertService.FireAsync("Error", mensajeError, SweetAlertIcon.Error);
            }
            return;
        }

        await LoadAsync();
        var toast = SweetAlertService.Mixin(new SweetAlertOptions
        {
            Toast = true,
            Position = SweetAlertPosition.BottomEnd,
            ShowConfirmButton = true,
            Timer = 3000
        });
        await toast.FireAsync(icon: SweetAlertIcon.Success, message: "Registro borrado con �xito.");
    }
}