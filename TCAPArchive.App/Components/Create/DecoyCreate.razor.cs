using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;
using Radzen;
using TCAPArchive.App.Services;
using TCAPArchive.Shared.Domain;
using TCAPArchive.Shared.ViewModels;

namespace TCAPArchive.App.Components.Create
{
    public partial class DecoyCreate
    {
        [Inject]
        public IDecoyDataService? DecoyDataService { get; set; }
        [Inject]
        public IPredatorDataService? PredatorDataService { get; set; }
        [Inject]
        public NavigationManager NavigationManager { get; set; }
        public Decoy decoy { get; set; } = new Decoy();

        public List<Predator> predators { get; set; }

        protected string Message = string.Empty;
        protected string StatusClass = string.Empty;
        protected bool busy;


        protected override async Task OnInitializedAsync()
        {
            predators = (await PredatorDataService.GetAllPredators()).ToList();
        }

        protected async Task HandleValidSubmit()
        {
            busy = true;

            if (selectedFileDecoy != null)
            {
                var file = selectedFileDecoy;
                Stream stream = file.OpenReadStream();
                MemoryStream ms = new();
                await stream.CopyToAsync(ms);
                stream.Close();
                decoy.ImageTitle = file.Name;
                decoy.ImageData = ms.ToArray();
            }
            decoy.Id = Guid.NewGuid();
            var addedDecoy = await DecoyDataService.AddDecoy(decoy);
            busy = false;
            if (addedDecoy != null)
            {
                var message = new NotificationMessage { Style = "position: fixed; top: 0; right: 0", Severity = NotificationSeverity.Success, Summary = "Success", Detail = "Successfully added decoy", Duration = 5000 };
                NotificationService.Notify(message);
            }
            else
            {
                var message = new NotificationMessage { Style = "position: fixed; top: 0; right: 0", Severity = NotificationSeverity.Error, Summary = "Failure", Detail = "Failed to add decoy", Duration = 5000 };
                NotificationService.Notify(message);
            }

            dialogService.Close();
        }

        private IBrowserFile selectedFileDecoy;


        private void OnInputFileChangeDecoy(InputFileChangeEventArgs e)
        {
            selectedFileDecoy = e.File;
            StateHasChanged();
        }

        protected async Task HandleInvalidSubmit()
        {
            StatusClass = "alert-danger";
            Message = "There are some validation errors. Please try again.";
        }

        protected void NavigateToOverview()
        {
            NavigationManager.NavigateTo("/decoys");
        }

    }
}
