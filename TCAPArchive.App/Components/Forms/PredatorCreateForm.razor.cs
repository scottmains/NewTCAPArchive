using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;
using TCAPArchive.App.Services;
using TCAPArchive.Shared.Domain;
using TCAPArchive.Shared.ViewModels;

namespace TCAPArchive.App.Components.Forms
{
    public partial class PredatorCreateForm
    {
        [Inject]
        public IPredatorDataService? PredatorDataService { get; set; }
        [Inject]
        public IDecoyDataService? DecoyDataService { get; set; }
        [Inject]
        public NavigationManager NavigationManager { get; set; }
        public Predator predator { get; set; } = new Predator();
        public CreatePredatorViewModel createPredator { get; set; }

        protected string Message = string.Empty;
        protected string StatusClass = string.Empty;
        protected bool Saved;


        protected async Task HandleValidSubmit()
        {
            Saved = false;
            var addPredator = new Predator
            {
                Id = new Guid(),
                FirstName = createPredator.FirstName,
                LastName = createPredator.LastName,
                Handle = createPredator.Handle,
                Description = createPredator.Description,
                StingLocation = createPredator.StingLocation,
            };

            var addDecoy = new Decoy
            {
                Id = new Guid(),
                PredatorId = addPredator.Id,
                Handle = createPredator.DecoyHandle
            };

            if (selectedFilePredator != null)
            {
                var file = selectedFilePredator;
                Stream stream = file.OpenReadStream();
                MemoryStream ms = new();
                await stream.CopyToAsync(ms);
                stream.Close();
                addPredator.ImageTitle = file.Name;
                addPredator.ImageData = ms.ToArray();
            }

            if (selectedFileDecoy != null)
            {
                var file = selectedFilePredator;
                Stream stream = file.OpenReadStream();
                MemoryStream ms = new();
                await stream.CopyToAsync(ms);
                stream.Close();
                addDecoy.ImageTitle = file.Name;
                addDecoy.ImageData = ms.ToArray();
            }

            var addedPredator = await PredatorDataService.AddPredator(addPredator);
            var addedDecoy = await DecoyDataService.AddDecoy(addDecoy);


            if (addedPredator != null && addedDecoy != null)
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
        private IBrowserFile selectedFileDecoy;

        private void OnInputFileChangePredator(InputFileChangeEventArgs e)
        {
            selectedFilePredator = e.File;
			StateHasChanged();
		}

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
            NavigationManager.NavigateTo("/predators");
        }

    }
}
