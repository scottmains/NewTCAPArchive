using Microsoft.AspNetCore.Components;
using Radzen;
using System;
using System.Globalization;
using System.Text.RegularExpressions;
using TCAPArchive.App.Services;
using TCAPArchive.Shared.Domain;
using TCAPArchive.Shared.ViewModels;
using static System.Runtime.InteropServices.JavaScript.JSType;

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
        public Predator predator { get; set; }
        public Decoy decoy { get; set; }
        protected bool busy;


        protected override async Task OnInitializedAsync()
        {
            await base.OnInitializedAsync();

            chatLine = await ChatlogDataService.GetChatLineById(ChatLineId);

            var chatParticipants = await ChatlogDataService.GetChatSessionById(chatLine.ChatSessionId);

            predator = await PredatorDataService.GetPredatorById(chatParticipants.PredatorId);
            decoy = await DecoyDataService.GetDecoyById(chatParticipants.DecoyId);

            var participantsList = addParticipantsToList(predator, decoy);

            senders = participantsList;

        }

        void OnChange(string value, string name)
        {
            string format = "MM/dd/yy hh:mm:ss tt";
            var formatInfo = new DateTimeFormatInfo()
            {
                ShortDatePattern = format
            };

            newChatLine.TimeStamp = Convert.ToDateTime(value, formatInfo);
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

            SetUpNewChatLine(newChatLine, chatLine);
         
            var addedChatLine = await ChatlogDataService.InsertChatLine(newChatLine);
            busy = false;

            if (addedChatLine > 0)
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

        private void SetUpNewChatLine(ChatLine newChatLine, ChatLine chatLine)
        {
            newChatLine.Id = Guid.NewGuid();
            newChatLine.Position = chatLine.Position + 1;
            newChatLine.ChatSessionId = chatLine.ChatSessionId;

            if (newChatLine.SenderId == predator.Id)
            {
                newChatLine.SenderHandle = predator.Handle;
            }
            else
            {
                newChatLine.SenderHandle = decoy.Handle;
            }
        }
    }
}
