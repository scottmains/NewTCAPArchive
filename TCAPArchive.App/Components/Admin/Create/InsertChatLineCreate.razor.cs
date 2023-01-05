using Microsoft.AspNetCore.Components;
using Radzen;
using System;
using TCAPArchive.App.Services;
using TCAPArchive.Shared.Domain;
using TCAPArchive.Shared.ViewModels;

namespace TCAPArchive.App.Components.Admin.Create
{
    public partial class InsertChatLineCreate
    {

        [Inject]
        public IChatlogDataService? ChatlogDataService { get; set; }
        [Inject]
        public IDecoyDataService? DecoyDataService { get; set; }
        [Inject]
        public IPredatorDataService? PredatorDataService { get; set; }
        [Parameter]
        public Guid ChatLineId { get; set; }
        public ChatLine chatLine { get; set; } 
        public ChatLine newChatLine { get; set; }   = new ChatLine();
        public List<AdminChatLineEditViewModel> senders { get; set; }
        protected bool busy;


        protected override async Task OnInitializedAsync()
        {
            await base.OnInitializedAsync();

            chatLine = await ChatlogDataService.GetChatLineById(ChatLineId);

            var chatParticipants = await ChatlogDataService.GetChatSessionById(chatLine.ChatSessionId);

            var predator = await PredatorDataService.GetPredatorById(chatParticipants.PredatorId);
            var decoy = await DecoyDataService.GetDecoyById(chatParticipants.DecoyId);

            var participantsList = addParticipantsToList(predator, decoy);

            senders = participantsList;

        }

        void OnChange(DateTime? value, string name, string format)
        {
            
        }

        private List<AdminChatLineEditViewModel> addParticipantsToList(Predator predator, Decoy decoy)
        {
            var addParticipants = new List<AdminChatLineEditViewModel>();

            var predatorSender = new AdminChatLineEditViewModel
            {
                senderHandle = predator.Handle,
                senderId = predator.Id
            };
            var decoySender = new AdminChatLineEditViewModel
            {
                senderHandle = decoy.Handle,
                senderId = decoy.Id
            };
            addParticipants.Add(predatorSender);
            addParticipants.Add(decoySender);

            return addParticipants;
        }

        protected async Task HandleValidSubmit()
        {
            busy = true;
            newChatLine.Id = Guid.NewGuid();
            newChatLine.Position = chatLine.Position + 1;

            var addedChatLine = await ChatlogDataService.InsertChatLine(newChatLine);
            busy = false;

            if (addedChatLine != null)
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
