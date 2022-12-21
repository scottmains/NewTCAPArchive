using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;
using TCAPArchive.App.Services;
using TCAPArchive.Shared.Domain;
using TCAPArchive.Shared.ViewModels;

namespace TCAPArchive.App.Components.Admin
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
        protected bool Saved;


        protected override async Task OnInitializedAsync()
        {
            predators = (await PredatorDataService.GetAllPredators()).ToList();
        }

        protected async Task HandleValidSubmit()
        {
            Saved = false;

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

            var addedDecoy = await DecoyDataService.AddDecoy(decoy);

            if (addedDecoy != null)
            {
                StatusClass = "alert-success";
                Message = "New decoy added successfully.";
                Saved = true;
            }
            else
            {
                StatusClass = "alert-danger";
                Message = "Something went wrong adding the new decoy. Please try again.";
                Saved = false;
            }
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
