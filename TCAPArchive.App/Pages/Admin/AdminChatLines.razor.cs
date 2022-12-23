using Microsoft.AspNetCore.Components;
using TCAPArchive.App.Services;
using TCAPArchive.Shared.Domain;
using TCAPArchive.Shared.ViewModels;

namespace TCAPArchive.App.Pages.Admin
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

        public List<ChatLine> ChatLines { get; set; }
        public Predator predator { get; set; }
        public Decoy decoy { get; set; }
        public ChatSession chatsession { get; set; }

        public List<AdminEditChatLinesViewModel> adminChatlines { get; set; }

        override protected async Task OnInitializedAsync()
        {
            await base.OnInitializedAsync();

            var newChatLines = new List<AdminEditChatLinesViewModel>();
            chatsession = (await ChatlogDataService.GetChatSessionById(ChatSessionId));
            ChatLines = (await ChatlogDataService.GetAllChatLinesByChatSession(ChatSessionId)).OrderBy(x=> x.Position).ToList();
            predator = (await PredatorDataService.GetPredatorById(chatsession.PredatorId));
            decoy = (await DecoyDataService.GetDecoyById(chatsession.DecoyId));

            foreach(var chatline in ChatLines)
            {
                byte[] imageData = null;

                if(chatline.SenderId == predator.Id)
                {
                    imageData = predator.ImageData;
                }
                else
                {
                    imageData = decoy.ImageData;
                }
                var adminChatLine = new AdminEditChatLinesViewModel
                {
                    chatLine = chatline,
                    ImageData = imageData
                };

                newChatLines.Add(adminChatLine);
            }

            adminChatlines = newChatLines;

        }

    }
}
