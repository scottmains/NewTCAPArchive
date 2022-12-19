using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;
using TCAPArchive.App.Services;
using TCAPArchive.Shared.Domain;

namespace TCAPArchive.App.Components
{
    public partial class DecoyCreateForm
    {
        [Inject]
        public IDecoyDataService? DecoyDataService { get; set; }

       
        public Decoy Decoy { get; set; } = new Decoy();

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
                Decoy.ImageTitle = file.Name;
                Decoy.ImageData = ms.ToArray();
            }
            Decoy.Id = Guid.NewGuid();
            var addedDecoy = await DecoyDataService.AddDecoy(Decoy);

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


    }
}
