using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.FileSystemGlobbing;
using Radzen;
using System.Globalization;
using System.Text.RegularExpressions;
using TCAPArchive.App.Pages;
using TCAPArchive.App.Pages.Admin;
using TCAPArchive.App.Services;
using TCAPArchive.Shared.Domain;
using TCAPArchive.Shared.ViewModels;

namespace TCAPArchive.App.Components.Admin
{
    public partial class ChatLinesCreate
    {

        [Inject]
        public IChatlogDataService? ChatlogDataService { get; set; }
        [Inject]
        public IDecoyDataService? DecoyDataService { get; set; }
        [Inject]
        public IPredatorDataService? PredatorDataService { get; set; }
        [Parameter]
        public Guid chatSessionId { get; set; }

        public AdminCreateChatLinesViewModel chatlines { get; set; } = new AdminCreateChatLinesViewModel();
        public Decoy decoy { get; set; }
        public Predator predator { get; set; }
        public ChatSession chatsession { get; set; }


        protected string Message = string.Empty;
        protected string StatusClass = string.Empty;
        protected bool busy;

        protected override async Task OnInitializedAsync()
        {

            await base.OnInitializedAsync();

            chatsession = (await ChatlogDataService.GetChatSessionById(chatSessionId));
            predator = await PredatorDataService.GetPredatorById(chatsession.PredatorId);
            decoy = await DecoyDataService.GetDecoyById(chatsession.DecoyId);
        }

        protected async Task HandleValidSubmit()
        {
            busy = true;
            string chatLogFormat1 = @"(\w+) \((\d+/\d+/\d+ \d+:\d+:\d+ [AP]M)\): (.*)";
            MatchCollection matches = Regex.Matches(chatlines.chatlog, chatLogFormat1);

            if (matches.Count > 0)
            {
                var chatlines = addLogWithFormat1(matches, chatsession.Id, predator, decoy);
                var addedChatLinesCount = await ChatlogDataService.AddChatLines(chatlines);
                chatsession.ChatLength = addedChatLinesCount;
                await ChatlogDataService.UpdateChatSession(chatsession);
                busy = false;
                if (addedChatLinesCount > 0)
                {
                    var message = new NotificationMessage { Style = "position: fixed; top: 0; right: 0", Severity = NotificationSeverity.Success, Summary = "Success", Detail = "Successfully added chatlog", Duration = 5000 };
                    NotificationService.Notify(message);
                }
                else
                {
                    var message = new NotificationMessage { Style = "position: fixed; top: 0; right: 0", Severity = NotificationSeverity.Error, Summary = "Failure", Detail = "Failed to add chatlog", Duration = 5000 };
                    NotificationService.Notify(message);
                }


                dialogService.Close();
            }
        }


        private List<ChatLine> addLogWithFormat1(MatchCollection matches, Guid ChatSessionId, Predator predator, Decoy decoy)
        {

            string username = "";
            DateTime date = DateTime.MinValue;
            string message = "";
            var counter = 1;
            var chatlines = new List<ChatLine>();

            foreach (Match match in matches)
            {
                var chatLine = new ChatLine();
                chatLine.Id = Guid.NewGuid();
                chatLine.ChatSessionId = ChatSessionId;
            
                string format = "MM/dd/yy hh:mm:ss tt";
                var formatInfo = new DateTimeFormatInfo()
                {
                    ShortDatePattern = format
                };

                username = match.Groups[1].Value;
                date = Convert.ToDateTime(match.Groups[2].Value, formatInfo);
                message = match.Groups[3].Value;

                if (username == predator.Handle)
                {
                    chatLine.SenderId = predator.Id;
                    chatLine.SenderHandle = predator.Handle;
                }
                else if (username == decoy.Handle)
                {
                    chatLine.SenderId = decoy.Id;
                    chatLine.SenderHandle = decoy.Handle;
                }
                else
                {
                    break;
                }

                chatLine.TimeStamp = date;
                chatLine.Message = message;
                chatLine.Position = counter;

                chatlines.Add(chatLine);

                counter++;
            }

            return chatlines;
        }


    }
}
