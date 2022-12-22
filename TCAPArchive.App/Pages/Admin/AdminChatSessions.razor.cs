using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Mvc;
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

        protected async Task OnAfterRenderAsync()
        {
            await InvokeAsync(() =>
            {
                StateHasChanged();
            });
        }

            public async Task OpenChatSessionCreate()
        {
           var result = await DialogService.OpenAsync<ChatSessionCreate>($" Create ",
                   new Dictionary<string, object>() { },
                   new DialogOptions() { Width = "700px", Height = "512px", Resizable = true, Draggable = true });

            if (result != null)
            {
                var message = new NotificationMessage { Style = "position: fixed; top: 0; right: 0", Severity = NotificationSeverity.Success, Summary = "Success", Detail = result, Duration = 5000 };
                NotificationService.Notify(message);
                StateHasChanged();
            }
        }

        public async Task AddChatlogClick(Guid chatSessionId)
        {
            var result = await DialogService.OpenAsync<ChatLinesCreate>($" Add Chatlog ",
                   new Dictionary<string, object>() { { "chatSessionId", chatSessionId } },
                   new DialogOptions() { Width = "700px", Height = "512px", Resizable = true, Draggable = true });

            if (result != null)
            {
                var message = new NotificationMessage { Style = "position: fixed; top: 0; right: 0", Severity = NotificationSeverity.Success, Summary = "Success", Detail = result, Duration = 5000 };
                NotificationService.Notify(message);
                StateHasChanged();
            }
        }


        public async Task DeleteButtonClick(Guid chatSessionId)
        {
            // Ask for confirmation:
            var confirmResult = await DialogService.Confirm(
                "Are you sure?", "Delete Chat Session");

            if (confirmResult.HasValue && confirmResult.Value)
            {
                try
                {
                   await ChatlogDataService.DeleteChatSession(chatSessionId);
                  
                
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
