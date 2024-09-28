using HostMaster.Shared.Resources;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;
using Microsoft.Extensions.Localization;
using System;

namespace HostMaster.Frontend.Shared;

public partial class MultipleInputImg
{
    private List<string> imagesBase64 = new List<string>(); 
    private List<string> fileNames = new List<string>();    

    [Inject] private IStringLocalizer<Literals> Localizer { get; set; } = null!;

    [Parameter] public string? Label { get; set; }
    [Parameter] public string? ImageURL { get; set; }       
    [Parameter] public EventCallback<List<string>> ImagesSelected { get; set; }  

    protected override void OnParametersSet()
    {
        base.OnParametersSet();
        if (string.IsNullOrWhiteSpace(Label))
        {
            Label = Localizer["Image"];
        }
    }
    
    private async Task OnChange(InputFileChangeEventArgs e)
    {
        fileNames.Clear();      
        imagesBase64.Clear();   

        var maxFiles = 3; 
        var maxAllowedSize = 5 * 1024 * 1024; 

        var selectedFiles = e.GetMultipleFiles().Take(maxFiles); 

        if (e.GetMultipleFiles().Count > maxFiles)
        {
            Console.WriteLine($"Only a maximum is allowed {maxFiles} files.");
        }

        foreach (var file in selectedFiles)
        {
            try
            {
                fileNames.Add(file.Name);               

                
                if (file.Size > maxAllowedSize)
                {
                    Console.WriteLine($"the file {file.Name} exceed the max size 5 MB.");
                    continue; 
                }

                using var stream = file.OpenReadStream(maxAllowedSize);
                using var memoryStream = new MemoryStream();

                
                await stream.CopyToAsync(memoryStream);
                byte[] arrBytes = memoryStream.ToArray();
                string base64String = Convert.ToBase64String(arrBytes);
                imagesBase64.Add(base64String);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error processing the file {file.Name}: {ex.Message}");
            }
        }

        ImageURL = null;
        await ImagesSelected.InvokeAsync(imagesBase64);
        StateHasChanged();
    }

}