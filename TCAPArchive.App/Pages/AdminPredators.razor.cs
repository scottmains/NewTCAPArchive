using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using Radzen;
using Radzen.Blazor;
using TCAPArchive.App.Components;
using TCAPArchive.App.Components.Create;
using TCAPArchive.App.Components.Edit;
using TCAPArchive.App.Services;
using TCAPArchive.Shared.Domain;

namespace TCAPArchive.App.Pages
{
    public partial class AdminPredators 
    {
        [Inject]
        public IPredatorDataService? PredatorDataService { get; set; }
        public List<Predator> Predators { get; set; }
        public Predator predator { get; set; }
 
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
                   new DialogOptions() { Width = "700px", Height = "600px", Resizable = true, Draggable = true, ShowClose = true });
                await RefreshData();
        }

        public async Task OpenDeleteDialog(Guid predatorId, string predatorName)
        {
            await DialogService.OpenAsync<DeleteDialog>($" Delete {predatorName}",
                   new Dictionary<string, object>() { { "predatorId", predatorId } },
                   new DialogOptions() { Width = "500px", Height = "200px", Resizable = true, Draggable = true });

            await RefreshData();

        }

  

    }

    }

