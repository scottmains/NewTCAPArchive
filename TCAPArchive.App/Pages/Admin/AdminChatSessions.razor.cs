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
      

        public List<AdminChatSessionViewModel> adminChatSessions { get; set; } 


        protected override async Task OnInitializedAsync()
        {
            ChatSessions = (await ChatlogDataService.GetAllChatSessions()).ToList();

            foreach(var chatSession in ChatSessions)
            {

               Decoy decoy = (await DecoyDataService.GetDecoyById(chatSession.DecoyId));
               Predator predator = (await PredatorDataService.GetPredatorById(chatSession.PredatorId));

                var viewModel = new AdminChatSessionViewModel
                {
                    decoy = decoy,
                    predator = predator,
                    ChatSession= chatSession    
                };

                adminChatSessions.Add(viewModel);
            }
        }



        public async Task OpenChatSessionCreate()
        {
            await DialogService.OpenAsync<ChatlogCreate>($" Create ",
                   new Dictionary<string, object>() { },
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
