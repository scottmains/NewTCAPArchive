using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;
using TCAPArchive.App.Services;
using TCAPArchive.Shared.Domain;

namespace TCAPArchive.App.Components
{
    public partial class PredatorCreateForm
    {
        [Inject]
        public IPredatorDataService? PredatorDataService { get; set; }

        [Inject]
        public NavigationManager NavigationManager { get; set; }
        public Predator Predator { get; set; } = new Predator();

        protected string Message = string.Empty;
        protected string StatusClass = string.Empty;
        protected bool Saved;


        protected async Task HandleValidSubmit()
        {
            Saved = false;

            if (selectedFile != null)
            {
                var file = selectedFile;
                Stream stream = file.OpenReadStream();
                MemoryStream ms = new();
                await stream.CopyToAsync(ms);
                stream.Close();
                Predator.ImageTitle = file.Name;
                Predator.ImageData = ms.ToArray();
            }
            Predator.Id = Guid.NewGuid();
            var addedPredator = await PredatorDataService.AddPredator(Predator);

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

        private IBrowserFile selectedFile;

        private void OnInputFileChange(InputFileChangeEventArgs e)
        {
            selectedFile = e.File;
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
