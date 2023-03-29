using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using Radzen;
using Radzen.Blazor;
using System.Numerics;
using TCAPArchive.App.Components;
using TCAPArchive.App.Components.Create;
using TCAPArchive.App.Services;
using TCAPArchive.Shared.Domain;
using TCAPArchive.Shared.ViewModels;

namespace TCAPArchive.App.Pages
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
        protected string Message = string.Empty;
        protected string StatusClass = string.Empty;
        protected bool Saved;
        protected bool busy;
        public List<AdminChatSessionViewModel> adminChatSessions { get; set; }


        override protected async Task OnInitializedAsync()
        {

            var allChatSessions = new List<AdminChatSessionViewModel>();

            ChatSessions = (await ChatlogDataService.GetAllChatSessions()).ToList();

            foreach (var chatSession in ChatSessions)
            {
                Decoy decoy = (await DecoyDataService.GetDecoyById(chatSession.DecoyId));
                Predator predator = (await PredatorDataService.GetPredatorById(chatSession.PredatorId));

                var viewModel = new AdminChatSessionViewModel
                {
                    chatsession = chatSession,
                    DecoyName = decoy.Handle,
                    PredatorName = predator.FirstName + " " + predator.LastName,
                    ImageData = predator.ImageData,
                    LineCount = chatSession.ChatLength
                };

                allChatSessions.Add(viewModel);
            }

            // manually trigger a re-render of the component after populating the adminChatSessions list
                adminChatSessions = allChatSessions;
        }

        public async Task RefreshData()
        {
            ChatSessions = (await ChatlogDataService.GetAllChatSessions()).ToList();
            var allChatSessions = new List<AdminChatSessionViewModel>();
            foreach (var chatSession in ChatSessions)
            {
                Decoy decoy = (await DecoyDataService.GetDecoyById(chatSession.DecoyId));
                Predator predator = (await PredatorDataService.GetPredatorById(chatSession.PredatorId));

                var viewModel = new AdminChatSessionViewModel
                {
                    chatsession = chatSession,
                    DecoyName = decoy.Handle,
                    PredatorName = predator.FirstName + " " + predator.LastName,
                    ImageData = predator.ImageData,
                    LineCount = chatSession.ChatLength
                };

                allChatSessions.Add(viewModel);
            }
            adminChatSessions = allChatSessions;
        }

        public async Task OpenChatSessionCreate()
        {
           var result = await DialogService.OpenAsync<ChatSessionCreate>($" Create ",
                   new Dictionary<string, object>() { },
                   new DialogOptions() { Width = "700px", Height = "512px", Resizable = true, Draggable = true });
               await RefreshData();
            }
        
        public async Task AddChatlogClick(Guid chatSessionId)
        {
            var result = await DialogService.OpenAsync<ChatLinesCreate>($" Add Chatlog ",
                   new Dictionary<string, object>() { { "chatSessionId", chatSessionId } },
                   new DialogOptions() { Width = "700px", Height = "512px", Resizable = true, Draggable = true });
                await RefreshData();
            }

        public async Task OpenDeleteDialog(Guid chatSessionId, string chatSessionName)
        {
            await DialogService.OpenAsync<DeleteDialog>($" Delete {chatSessionName}",
                   new Dictionary<string, object>() { { "ChatSessionId", chatSessionId } },
                   new DialogOptions() { Width = "500px", Height = "200px", Resizable = true, Draggable = true });
            await RefreshData();
        }

        protected void NavigateToChatSession(Guid ChatSessionId)
        {
            NavigationManager.NavigateTo("/chatlines/" + ChatSessionId);
        }
    }
}
