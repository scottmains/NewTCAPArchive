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
        protected bool Saved;
      

        protected override async Task OnInitializedAsync()
        {
            await base.OnInitializedAsync();

            predator = await PredatorDataService.GetPredatorById(predatorId);

        }

     

        protected async Task HandleValidSubmit()
        {

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

            await PredatorDataService.UpdatePredator(predator);

         
                StatusClass = "alert-success";
                Message = "Predator updated successfully.";
                Saved = true;

            if (Saved == true)
            {
                dialogService.Close(Message);
               
            }
        }

        private IBrowserFile selectedFilePredator;
        private void OnInputFileChangePredator(InputFileChangeEventArgs e)
        {
            selectedFilePredator = e.File;
            StateHasChanged();
        }
    }
}
