using Microsoft.AspNetCore.Components;
using Radzen;
using Radzen.Blazor;
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
        private RadzenDataGrid<Predator> DataGridRef { get; set; }
        protected string Message = string.Empty;
        protected string StatusClass = string.Empty;
        protected bool Saved;

        protected override async Task OnInitializedAsync()
        {
            Predators = (await PredatorDataService.GetAllPredators()).ToList();
        }

        public async Task OpenPredatorEdit(Guid predatorId, string predatorName)
        {
            await DialogService.OpenAsync<PredatorEdit> ($" Edit {predatorName}",
                   new Dictionary<string, object>() { { "predatorId", predatorId } },
                   new DialogOptions() { Width = "700px", Height = "512px", Resizable = true, Draggable = true });

                await RefreshData();
        }

        public async Task RefreshData()
        {
            Predators = (await PredatorDataService.GetAllPredators()).ToList();
        }

        public async Task OpenPredatorCreate()
        {
            
            var result = await DialogService.OpenAsync<PredatorCreate>($" Create ",
                   new Dictionary<string, object>() { },
                   new DialogOptions() { Width = "700px", Height = "512px", Resizable = true, Draggable = true, ShowClose = false });

        
                await RefreshData();
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
                   
                    var success = await PredatorDataService.DeletePredator(predatorId);

                    if (success > 0)
                    {
                        StatusClass = "alert-success";
                        Saved = true;
                    }

                    if (Saved)
                    {
                        var message = new NotificationMessage { Style = "position: fixed; top: 0; right: 0", Severity = NotificationSeverity.Success, Summary = "Success", Detail = "Succesfully deleted predator", Duration = 5000 };
                        NotificationService.Notify(message);
                    }
                    else
                    {
                        var message = new NotificationMessage { Style = "position: fixed; top: 0; right: 0", Severity = NotificationSeverity.Error, Summary = "Failure", Detail = "Failed to delete predator", Duration = 5000 };
                        NotificationService.Notify(message);
                    }


                    await RefreshData();
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
