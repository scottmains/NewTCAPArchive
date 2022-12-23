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

        protected string Message = string.Empty;
        protected string StatusClass = string.Empty;
        protected bool Saved;

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
        
        public async Task DeleteButtonClick(Guid chatSessionId)
        {

            Saved = false;
            // Ask for confirmation:
            var confirmResult = await DialogService.Confirm(
                "Are you sure?", "Delete Chat Session");

            if (confirmResult.HasValue && confirmResult.Value)
            {
                try
                {
                 var success =  await ChatlogDataService.DeleteChatSession(chatSessionId);

                    if (success > 0) {
                        StatusClass = "alert-success";
                        Message = "Chatsession deleted successfully.";
                        Saved = true;
                    }
                
                if (Saved == true)
                {
                 var message = new NotificationMessage { Style = "position: fixed; top: 0; right: 0", Severity = NotificationSeverity.Success, Summary = "Success", Detail = Message, Duration = 5000 };
                 NotificationService.Notify(message);
                 await RefreshData();
                }
                    else
                    {
                        var message = new NotificationMessage { Style = "position: fixed; top: 0; right: 0", Severity = NotificationSeverity.Error, Summary = "Failure", Detail = "Failed to delete chatsession", Duration = 5000 };
                        NotificationService.Notify(message);
                    }

                }
                catch (Exception exception)
                {
                    NotificationService.Notify(NotificationSeverity.Error, $"Error",
                        $"Foo", duration: -1);

                }

            }
        }

        protected void NavigateToChatSession(Guid ChatSessionId)
        {
            NavigationManager.NavigateTo("/chatlines/" + ChatSessionId);
        }
    }
}
