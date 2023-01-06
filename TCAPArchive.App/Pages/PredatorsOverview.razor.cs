using Microsoft.AspNetCore.Components;
using Radzen.Blazor;
using TCAPArchive.App.Services;
using TCAPArchive.Shared.Domain;


namespace TCAPArchive.App.Pages
{
    public partial class PredatorsOverview
    {
        [Inject]
        public IPredatorDataService? PredatorDataService { get; set; }
        [Inject]
        public IChatlogDataService ChatlogDataService { get; set; }
        public List<Predator> Predators { get; set; } = default!;
        public ChatSession ChatSession { get; set; }
        public List<Predator> filteredPredators;
        RadzenDataFilter<Predator> dataFilter { get; set; }
        public List<Predator> StingLocations { get; set; }
        bool busy { get; set; }

        protected override async Task OnInitializedAsync()
        {
         
            Predators = (await PredatorDataService.GetAllPredators()).ToList();
            StingLocations = Predators.DistinctBy(x => x.StingLocation).ToList();
           
        }


        void OnChangeFilter(string value)
        {
            value = value.ToLower();
            filteredPredators = Predators.Where(x => x.FirstName.ToLower().Contains(value) || x.LastName.ToLower().Contains(value)).ToList();
        }

        protected async void NavigateToChatSession(Guid PredatorId)
        {

            var chatsession = (await ChatlogDataService.GetChatSessionByPredatorId(PredatorId));

            NavigationManager.NavigateTo("/chatlines/" + chatsession.Id);
        }

    }
}
