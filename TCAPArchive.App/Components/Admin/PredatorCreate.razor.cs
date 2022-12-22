using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;
using TCAPArchive.App.Services;
using TCAPArchive.Shared.Domain;
using TCAPArchive.Shared.ViewModels;

namespace TCAPArchive.App.Components.Admin
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
        protected bool Saved;


        protected async Task HandleValidSubmit()
        {
            Saved = false;
      
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

            var addedPredator = await PredatorDataService.AddPredator(predator);

            if (addedPredator != null)
            {
                StatusClass = "alert-success";
                Message = "New predator added successfully.";
                Saved = true;
            }
            else
            {
                StatusClass = "alert-danger";
                Message = "Something went wrong adding the new predator. Please try again.";
                Saved = false;
            }
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
