using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;
using Radzen;
using System;
using System.Runtime.InteropServices;
using TCAPArchive.App.Pages.Admin;
using TCAPArchive.App.Services;
using TCAPArchive.Shared.Domain;
using static TCAPArchive.App.Pages.Admin.AdminPredators;

namespace TCAPArchive.App.Components.Admin
{
    public partial class PredatorEdit
    {

        [Inject]
        public IPredatorDataService? PredatorDataService { get; set; }

        [Parameter]
        public Guid predatorId { get; set; }
        public Predator predator { get; set; }

        protected string Message = string.Empty;
        protected string StatusClass = string.Empty;
        protected bool busy;


        protected override async Task OnInitializedAsync()
        {
            await base.OnInitializedAsync();

            predator = await PredatorDataService.GetPredatorById(predatorId);

        }

        protected async Task HandleValidSubmit()
        {
            busy = true;
            if (selectedFilePredator != null)
            {
                var file = selectedFilePredator;
                Stream stream = file.OpenReadStream();
                MemoryStream ms = new();
                await stream.CopyToAsync(ms);
                stream.Close();
                predator.ImageTitle = file.Name;
                predator.ImageData = ms.ToArray();
            }

                var success = await PredatorDataService.UpdatePredator(predator);
                busy = false;
            if (success > 0)
            {
                var message = new NotificationMessage { Style = "position: fixed; top: 0; right: 0", Severity = NotificationSeverity.Success, Summary = "Success", Detail = "Successfully edited predator", Duration = 5000 };
                NotificationService.Notify(message);
            }
            else
            {
                var message = new NotificationMessage { Style = "position: fixed; top: 0; right: 0", Severity = NotificationSeverity.Error, Summary = "Failure", Detail = "Failed to edit predator", Duration = 5000 };
                NotificationService.Notify(message);
            }

            dialogService.Close();
               
           
        }

        private IBrowserFile selectedFilePredator;
        private void OnInputFileChangePredator(InputFileChangeEventArgs e)
        {
            selectedFilePredator = e.File;
            StateHasChanged();
        }
    }
}
