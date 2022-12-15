using Microsoft.AspNetCore.Components;
using TCAPArchive.App.Services;
using TCAPArchive.Shared.Domain;


namespace TCAPArchive.App.Pages
{
    public partial class PredatorsOverview
    {
        [Inject]
        public IPredatorDataService? PredatorDataService { get; set; }
        public List<Predator> Predators { get; set; } = default!;

        protected override async Task OnInitializedAsync()
        {
            Predators = (await PredatorDataService.GetAllPredators()).ToList();
        }

    }
}
