using Blazorise;
using Microsoft.AspNetCore.Components;
using System.Diagnostics.Metrics;
using System.Globalization;
using System.Text.RegularExpressions;
using TCAPArchive.App.Pages;
using TCAPArchive.App.Services;
using TCAPArchive.Shared.Domain;
using static System.Runtime.InteropServices.JavaScript.JSType;
using Match = System.Text.RegularExpressions.Match;

namespace TCAPArchive.App.Components.Forms
{
    public partial class ChatlogCreateForm
    {
        [Inject]
        public IPredatorDataService? PredatorDataService { get; set; }

        [Inject]
        public IDecoyDataService? DecoyDataService { get; set; }
        [Inject]
        public IChatlogDataService? ChatlogDataService { get; set; }

        public Decoy SelectedDecoy { get; set; } = new Decoy();
        public Predator SelectedPredator { get; set; } = new Predator();
        public List<Predator> predators { get; set; } = new List<Predator>();
        public List<Decoy> decoys { get; set; } = new List<Decoy>();

        public ChatSession chatsession { get; set; } = new ChatSession();

        public string Chatlog { get; set; }

        protected string Message = string.Empty;
        protected string StatusClass = string.Empty;
        protected bool Saved;

        protected override async Task OnInitializedAsync()
        {
            predators = (await PredatorDataService.GetAllPredators()).ToList();
            decoys = (await DecoyDataService.GetAllDecoys()).ToList();
        }

        protected async Task HandleValidSubmit()
        {

            string chatLogFormat1 = @"(\w+) \((\d+/\d+/\d+ \d+:\d+:\d+ [AP]M)\): (.*)";
            MatchCollection matches = Regex.Matches(Chatlog, chatLogFormat1);
            var ChatLines = new List<ChatLine>();
            if (matches.Count > 0)
            {
                ChatLines = addLogWithFormat1(matches, ChatLines);
            }

            chatsession.Predator = PredatorDataService.GetPredatorById(SelectedPredator.Id).Result;

            var addedChatlog = await ChatlogDataService.AddChatSession(chatsession);

            if (addedChatlog != null)
            {
                StatusClass = "alert-success";
                Message = "New decoy added successfully.";
                Saved = true;
            }
            else
            {
                StatusClass = "alert-danger";
                Message = "Something went wrong adding the new decoy. Please try again.";
                Saved = false;
            }
        }

        private List<ChatLine> addLogWithFormat1(MatchCollection matches, List<ChatLine> ChatLines)
        {
            
            string username = "";
            DateTime date = DateTime.MinValue;
            string message = "";
            var counter = 1;
            foreach (Match match in matches)
            {
                var chatLine = new ChatLine();

                string format = "MM/dd/yy hh:mm:ss tt";
                var formatInfo = new DateTimeFormatInfo()
                {
                    ShortDatePattern = format
                };

                username = match.Groups[1].Value;
                date = Convert.ToDateTime(match.Groups[2].Value, formatInfo);
                message = match.Groups[3].Value;

                if (username == SelectedPredator.Handle)
                {
                    chatLine.SenderId = SelectedPredator.Id;
                    chatLine.SenderHandle = SelectedPredator.Handle;
                }

                if (username == SelectedDecoy.Handle)
                {
                    chatLine.SenderId = SelectedDecoy.Id;
                    chatLine.SenderHandle = SelectedDecoy.Handle;
                }

                chatLine.TimeStamp = date;
                chatLine.Message = message;
                chatLine.Position = counter;

                ChatLines.Add(chatLine); 

                counter++;
            }

            return ChatLines;
        }
    }
}
