
using Microsoft.AspNetCore.Components;
using System.Diagnostics.Metrics;
using System.Globalization;
using System.Text.RegularExpressions;
using TCAPArchive.App.Pages;
using TCAPArchive.App.Pages.Admin;
using TCAPArchive.App.Services;
using TCAPArchive.Shared.Domain;
using static System.Runtime.InteropServices.JavaScript.JSType;
using Match = System.Text.RegularExpressions.Match;

namespace TCAPArchive.App.Components.Admin
{
    public partial class ChatSessionCreate
    {
        [Inject]
        public IPredatorDataService? PredatorDataService { get; set; }

        [Inject]
        public IDecoyDataService? DecoyDataService { get; set; }
        [Inject]
        public IChatlogDataService? ChatlogDataService { get; set; }

        public Decoy SelectedDecoy { get; set; } = new Decoy();
        public Predator SelectedPredator { get; set; } = new Predator();
        public List<Predator> predators { get; set; } = new List<Predator>();
        public List<Decoy> decoys { get; set; } = new List<Decoy>();

        public ChatSession chatsession { get; set; } = new ChatSession();

        protected string Message = string.Empty;
        protected string StatusClass = string.Empty;
        protected bool Saved;

        protected override async Task OnInitializedAsync()
        {
            predators = (await PredatorDataService.GetAllPredators()).ToList();
            decoys = (await DecoyDataService.GetAllDecoys()).ToList();
        }

        protected async Task HandleValidSubmit()
        {

            var addedChatSession = await ChatlogDataService.AddChatSession(chatsession);

            if (addedChatSession != null)
            {
                StatusClass = "alert-success";
                Message = "New decoy added successfully.";
                Saved = true;
            }
            else
            {
                StatusClass = "alert-danger";
                Message = "Something went wrong adding the new decoy. Please try again.";
                Saved = false;
            }
        }

        }
    }

