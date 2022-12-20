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
            await DialogService.OpenAsync<PredatorEdit>($" Edit {predatorName}",
                   new Dictionary<string, object>() { { "predatorId", predatorId } },
                   new DialogOptions() { Width = "700px", Height = "512px", Resizable = true, Draggable = true });
        }

    }
}
