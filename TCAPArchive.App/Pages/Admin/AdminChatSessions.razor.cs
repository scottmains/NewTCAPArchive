using Microsoft.AspNetCore.Components;
using Radzen;
using TCAPArchive.App.Components.Admin;
using TCAPArchive.App.Services;
using TCAPArchive.Shared.Domain;
using TCAPArchive.Shared.ViewModels;

namespace TCAPArchive.App.Pages.Admin
{
    public partial class AdminChatSessions
    {
        [Inject]
        public IChatlogDataService? ChatlogDataService { get; set; }
        [Inject]
        public IDecoyDataService? DecoyDataService { get; set; }
        [Inject]
        public IPredatorDataService? PredatorDataService { get; set; }
        public List<ChatSession> ChatSessions { get; set; }
      

        public List<AdminChatSessionViewModel> adminChatSessions { get; set; } = new List<AdminChatSessionViewModel>();


        protected override async Task OnInitializedAsync()
        {
            ChatSessions = (await ChatlogDataService.GetAllChatSessions()).ToList();

            foreach(var chatSession in ChatSessions)
            {
               Decoy decoy = (await DecoyDataService.GetDecoyById(chatSession.DecoyId));
               Predator predator = (await PredatorDataService.GetPredatorById(chatSession.PredatorId));
                var LineCount = (await ChatlogDataService.GetAllChatLinesByChatSession(chatSession.Id)).Count();
                var viewModel = new AdminChatSessionViewModel
                {
                    decoy = decoy,
                    predator = predator,
                    ChatSession = chatSession,
                    LineCount = LineCount
            };

                adminChatSessions.Add(viewModel);
            }
        }
        public async Task OpenChatSessionCreate()
        {
            await DialogService.OpenAsync<ChatSessionCreate>($" Create ",
                   new Dictionary<string, object>() { },
                   new DialogOptions() { Width = "700px", Height = "512px", Resizable = true, Draggable = true });
        }

        public async Task AddChatlogClick(Guid chatSessionId)
        {
            await DialogService.OpenAsync<ChatLinesCreate>($" Add Chatlog ",
                   new Dictionary<string, object>() { { "chatSessionId", chatSessionId } },
                   new DialogOptions() { Width = "700px", Height = "512px", Resizable = true, Draggable = true });
        }


        public async Task DeleteButtonClick(Guid decoyId)
        {
            // Ask for confirmation:
            var confirmResult = await DialogService.Confirm(
                "Yes", "No");

            if (confirmResult.HasValue && confirmResult.Value)
            {
                try
                {
                   
                }
                catch (Exception exception)
                {
                    NotificationService.Notify(NotificationSeverity.Error, $"Error",
                        $"Foo", duration: -1);

                }

            }
        }
    }
}
