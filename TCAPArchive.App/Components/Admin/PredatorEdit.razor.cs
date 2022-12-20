using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;
using TCAPArchive.App.Services;
using TCAPArchive.Shared.Domain;

namespace TCAPArchive.App.Components.Admin
{
    public partial class PredatorEdit
    {

        [Inject]
        public IPredatorDataService? PredatorDataService { get; set; }

        [Parameter]
        public Guid predatorId { get; set; }
        public Predator predator { get; set; }

        protected override async Task OnInitializedAsync()
        {
            await base.OnInitializedAsync();

            predator = await PredatorDataService.GetPredatorById(predatorId);

         
        }


        protected async Task HandleValidSubmit()
        {
        }


        private IBrowserFile selectedFilePredator;
        private void OnInputFileChangePredator(InputFileChangeEventArgs e)
        {
            selectedFilePredator = e.File;
            StateHasChanged();
        }
    }
}
