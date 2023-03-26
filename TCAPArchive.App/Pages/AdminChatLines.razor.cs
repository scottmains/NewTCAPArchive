using Microsoft.AspNetCore.Components;
using TCAPArchive.App.Services;
using TCAPArchive.Shared.Domain;
using TCAPArchive.Shared.ViewModels;
using Radzen;
using TCAPArchive.App.Components.Edit;
using TCAPArchive.App.Components.Create;
using Radzen.Blazor;

namespace TCAPArchive.App.Pages
{
    public partial class AdminChatLines
    {
        [Inject]
        public IChatlogDataService? ChatlogDataService { get; set; }
        [Inject]
        public IPredatorDataService? PredatorDataService { get; set; }
        [Inject]
        public IDecoyDataService? DecoyDataService { get; set; }
        [Parameter]
        public Guid ChatSessionId { get; set; }

        RadzenDataGrid<ChatLinesViewModel> chatLineGrid;

        public List<ChatLine>? ChatLines { get; set; }
        public Predator? predator { get; set; }
        public Decoy? decoy { get; set; }
        public ChatSession? chatsession { get; set; }

        public List<AdminChatLineEditViewModel>? senders { get; set; }

        ChatLine? chatLineToInsert;
        ChatLine? chatLineToUpdate;

        public List<ChatLinesViewModel>? adminChatlines { get; set; }

        override protected async Task OnInitializedAsync()
        {
            await base.OnInitializedAsync();

            chatsession = (await ChatlogDataService.GetChatSessionById(ChatSessionId));
            ChatLines = (await ChatlogDataService.GetAllChatLinesByChatSession(ChatSessionId)).OrderBy(x=> x.Position).ToList();
            predator = (await PredatorDataService.GetPredatorById(chatsession.PredatorId));
            decoy = (await DecoyDataService.GetDecoyById(chatsession.DecoyId));

            var newChatLines = SetUpSender(ChatLines);
            adminChatlines = newChatLines;

            var participantsList = addParticipantsToList(predator, decoy);

            senders = participantsList;

        }

        private List<ChatLinesViewModel> SetUpSender(List<ChatLine> chatLines)
        {
            var newChatLines = new List<ChatLinesViewModel>();
            foreach (var chatline in ChatLines)
            {
                var adminChatLine = new ChatLinesViewModel();
                if (chatline.SenderId == predator.Id)
                {
                  adminChatLine.predator = predator;
                }
                else
                {
                    adminChatLine.decoy = decoy;
                }
                adminChatLine.chatLine = chatline;
                newChatLines.Add(adminChatLine);
            }

            return newChatLines;
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

        void Reset()
        {
            chatLineToInsert = null;
            chatLineToUpdate = null;
        }

        public async Task RefreshData()
        {
            ChatLines = (await ChatlogDataService.GetAllChatLinesByChatSession(ChatSessionId)).OrderBy(x => x.Position).ToList();
        }

        async Task EditRow(ChatLinesViewModel chatline)
        {
            chatLineToUpdate = chatline.chatLine;
            await chatLineGrid.EditRow(chatline);

        }

        async Task SaveRow(ChatLinesViewModel chatline)
        {
            await chatLineGrid.UpdateRow(chatline);
        }

        void OnUpdateRow(ChatLinesViewModel chatline)
        {
            if (chatline.chatLine == chatLineToInsert)
            {
                chatLineToInsert = null;
            }

            chatLineToUpdate = null;

            ChatlogDataService.UpdateChatLine(chatline.chatLine);

        }

        void CancelEdit(ChatLinesViewModel chatline)
        {
            if (chatline.chatLine == chatLineToInsert)
            {
                chatLineToInsert = null;
            }

            chatLineToUpdate = null;
            chatLineGrid.CancelEditRow(chatline);

        }
     

    }
}
