using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;
using Radzen;
using TCAPArchive.App.Services;
using TCAPArchive.Shared.Domain;

namespace TCAPArchive.App.Components.Edit
{
    public partial class DecoyEdit
    {

        [Inject]
        public IDecoyDataService? DecoyDataService { get; set; }

        [Parameter]
        public Guid decoyId { get; set; }
        public Decoy decoy { get; set; }

        public string Chatlog { get; set; }

        protected string Message = string.Empty;
        protected string StatusClass = string.Empty;
        protected bool busy;

        protected override async Task OnInitializedAsync()
        {
            await base.OnInitializedAsync();

            decoy = await DecoyDataService.GetDecoyById(decoyId);
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

            var success = await DecoyDataService.UpdateDecoy(decoy);
            busy= false;
            if (success > 0)
            {
                var message = new NotificationMessage { Style = "position: fixed; top: 0; right: 0", Severity = NotificationSeverity.Success, Summary = "Success", Detail = "Successfully edited decoy", Duration = 5000 };
                NotificationService.Notify(message);
            }
            else
            {
                var message = new NotificationMessage { Style = "position: fixed; top: 0; right: 0", Severity = NotificationSeverity.Error, Summary = "Failure", Detail = "Failed edit decoy", Duration = 5000 };
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
    }
}
