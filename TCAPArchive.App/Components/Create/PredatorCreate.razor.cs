using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;
using Radzen;
using TCAPArchive.App.Services;
using TCAPArchive.Shared.Domain;
using TCAPArchive.Shared.ViewModels;

namespace TCAPArchive.App.Components.Create
{
    public partial class PredatorCreate
    {
        [Inject]
        public IPredatorDataService? PredatorDataService { get; set; }
        [Inject]
        public IDecoyDataService? DecoyDataService { get; set; }
        [Inject]
        public NavigationManager NavigationManager { get; set; }
        public Predator predator { get; set; } = new Predator();

        protected string Message = string.Empty;
        protected string StatusClass = string.Empty;
        protected bool busy;


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
            predator.Id = Guid.NewGuid();
            var addedPredator = await PredatorDataService.AddPredator(predator);
            busy = false;

            if (addedPredator != null)
            {
                var message = new NotificationMessage { Style = "position: fixed; top: 0; right: 0", Severity = NotificationSeverity.Success, Summary = "Success", Detail = "Successfully added predator", Duration = 5000 };
                NotificationService.Notify(message);
            }
            else
            {
                var message = new NotificationMessage { Style = "position: fixed; top: 0; right: 0", Severity = NotificationSeverity.Error, Summary = "Failure", Detail = "Failed to add predator", Duration = 5000 };
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

        protected async Task HandleInvalidSubmit()
        {
            StatusClass = "alert-danger";
            Message = "There are some validation errors. Please try again.";
        }

        protected void NavigateToOverview()
        {
            NavigationManager.NavigateTo("/predators");
        }

    }
}
