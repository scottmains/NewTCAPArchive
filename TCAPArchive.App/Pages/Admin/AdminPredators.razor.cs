using Microsoft.AspNetCore.Components;
using Radzen;
using TCAPArchive.App.Components.Admin;
using TCAPArchive.App.Services;
using TCAPArchive.Shared.Domain;

namespace TCAPArchive.App.Pages.Admin
{
    public partial class AdminPredators
    {
        [Inject]
        public IPredatorDataService? PredatorDataService { get; set; }
        public List<Predator> Predators { get; set; }
        public Predator predator { get; set; }
     


        protected override async Task OnInitializedAsync()
        {
            Predators = (await PredatorDataService.GetAllPredators()).ToList();
        }

        public async Task OpenPredatorEdit(Guid predatorId, string predatorName)
        {
            var result= await DialogService.OpenAsync<PredatorEdit> ($" Edit {predatorName}",
                   new Dictionary<string, object>() { { "predatorId", predatorId } },
                   new DialogOptions() { Width = "700px", Height = "512px", Resizable = true, Draggable = true });

            if (result != null)
            {
                var message = new NotificationMessage { Style = "position: fixed; top: 0; right: 0", Severity = NotificationSeverity.Success, Summary = "Success", Detail = result, Duration = 5000 };
                NotificationService.Notify(message);
                StateHasChanged();
               
            }
      
        }

        public async Task OpenPredatorCreate()
        {
            
            var result = await DialogService.OpenAsync<PredatorCreate>($" Create ",
                   new Dictionary<string, object>() { },
                   new DialogOptions() { Width = "700px", Height = "512px", Resizable = true, Draggable = true, ShowClose = false }); 

        }

        public async Task DeleteButtonClick(Guid predatorId)
        {
            // Ask for confirmation:
            var confirmResult = await DialogService.Confirm(
                "Deleting this predator will delete all related chat sessions, are you sure?", "Delete Predator");

            if (confirmResult.HasValue && confirmResult.Value)
            {
                try
                {
                    Predators.RemoveAll(x => x.Id == predatorId);
                    await PredatorDataService.DeletePredator(predatorId);

                    NotificationService.Notify(NotificationSeverity.Info, $"Delete Successful",
                        $"Predator has been deleted", duration: 4000);
                }
                catch (Exception exception)
                {
                    NotificationService.Notify(NotificationSeverity.Error, $"Error",
                        $"{exception}", duration: -1);

                }

            }
        }

    }
}
