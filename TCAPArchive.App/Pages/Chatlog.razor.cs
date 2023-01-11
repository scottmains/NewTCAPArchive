using Microsoft.AspNetCore.Components;
using TCAPArchive.App.Pages.Admin;
using TCAPArchive.App.Services;
using TCAPArchive.Shared.Domain;
using TCAPArchive.Shared.ViewModels;

namespace TCAPArchive.App.Pages
{
	public partial class Chatlog
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

        public List<ChatLinesViewModel> chatlines { get; set; }
        protected async override Task OnInitializedAsync()
		{
            await base.OnInitializedAsync();

         
            chatsession = (await ChatlogDataService.GetChatSessionById(ChatSessionId));
            ChatLines = (await ChatlogDataService.GetAllChatLinesByChatSession(ChatSessionId)).OrderBy(x => x.Position).ToList();
            predator = (await PredatorDataService.GetPredatorById(chatsession.PredatorId));
            decoy = (await DecoyDataService.GetDecoyById(chatsession.DecoyId));

            var newChatLines = SetUpSender(ChatLines);
            chatlines = newChatLines;
            chatlines = newChatLines;
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
                newChatLines.Add(adminChatLine);
            }

            return newChatLines;
        }
    }



	}

