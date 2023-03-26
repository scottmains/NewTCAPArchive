using Microsoft.AspNetCore.Components;
using Radzen;
using TCAPArchive.App.Services;
using TCAPArchive.Shared.Domain;
using TCAPArchive.Shared.ViewModels;

namespace TCAPArchive.App.Components.Edit
{
    public partial class ChatLineEdit
    {
        [Parameter]
        public Guid ChatLineId { get; set; }

        [Inject]
        public IChatlogDataService? ChatlogDataService { get; set; }
        [Inject]
        public IDecoyDataService? DecoyDataService { get; set; }
        [Inject]
        public IPredatorDataService? PredatorDataService { get; set; }

        public ChatLine chatLine { get; set; }
        public bool busy { get; set; }
        public List<AdminChatLineEditViewModel> senders { get; set; }

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
       
            var success = await ChatlogDataService.UpdateChatLine(chatLine);

            busy = false;
            if (success > 0)
            {
                var message = new NotificationMessage { Style = "position: fixed; top: 0; right: 0", Severity = NotificationSeverity.Success, Summary = "Success", Detail = "Successfully edited decoy", Duration = 5000 };
                NotificationService.Notify(message);
            }
            else
            {
                var message = new NotificationMessage { Style = "position: fixed; top: 0; right: 0", Severity = NotificationSeverity.Error, Summary = "Failure", Detail = "Failed edit decoy", Duration = 5000 };
                NotificationService.Notify(message);
            }


            dialogService.Close();

        }

    }
}
