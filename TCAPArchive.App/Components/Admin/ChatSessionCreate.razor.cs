
using Microsoft.AspNetCore.Components;
using Radzen;
using System.Diagnostics.Metrics;
using System.Globalization;
using System.Text.RegularExpressions;
using TCAPArchive.App.Pages;
using TCAPArchive.App.Pages.Admin;
using TCAPArchive.App.Services;
using TCAPArchive.Shared.Domain;
using TCAPArchive.Shared.ViewModels;
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

        public ChatSession chatsession {get; set;} = new ChatSession();

        protected string Message = string.Empty;
        protected string StatusClass = string.Empty;
        protected bool busy;

        protected override async Task OnInitializedAsync()
        {
            predators = (await PredatorDataService.GetAllPredators()).ToList();
            decoys = (await DecoyDataService.GetAllDecoys()).ToList();
        }

        protected async Task HandleValidSubmit()
        {
            busy = true;
            chatsession.Id = Guid.NewGuid();
            var addedChatSession = await ChatlogDataService.AddChatSession(chatsession);
            busy = false;

            if (addedChatSession != null)
            {
                var message = new NotificationMessage { Style = "position: fixed; top: 0; right: 0", Severity = NotificationSeverity.Success, Summary = "Success", Detail = "Successfully added chat session", Duration = 5000 };
                NotificationService.Notify(message);
            }
            else
            {
                var message = new NotificationMessage { Style = "position: fixed; top: 0; right: 0", Severity = NotificationSeverity.Error, Summary = "Failure", Detail = "Failed to add chat session", Duration = 5000 };
                NotificationService.Notify(message);
            }

            dialogService.Close();
        }

        }
    }

