using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;
using TCAPArchive.App.Services;
using TCAPArchive.Shared.Domain;

namespace TCAPArchive.App.Components.Admin
{
    public partial class DecoyEdit
    {

        [Inject]
        public IDecoyDataService? DecoyDataService { get; set; }

        [Parameter]
        public Guid decoyId { get; set; }
        public Decoy decoy { get; set; }

        protected string Message = string.Empty;
        protected string StatusClass = string.Empty;
        protected bool Saved;

        protected override async Task OnInitializedAsync()
        {
            await base.OnInitializedAsync();

            decoy = await DecoyDataService.GetDecoyById(decoyId);
        }

        protected async Task HandleValidSubmit()
        {

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

            await DecoyDataService.UpdateDecoy(decoy);
            StatusClass = "alert-success";
            Message = "Employee updated successfully.";
            Saved = true;

        }

        private IBrowserFile selectedFileDecoy;
        private void OnInputFileChangeDecoy(InputFileChangeEventArgs e)
        {
            selectedFileDecoy = e.File;
            StateHasChanged();
        }
    }
}
