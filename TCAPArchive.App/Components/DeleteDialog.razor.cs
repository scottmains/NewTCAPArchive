using Microsoft.AspNetCore.Components;
using Radzen;
using TCAPArchive.App.Services;
using TCAPArchive.Shared.Domain;

namespace TCAPArchive.App.Components
{
    public partial class DeleteDialog
    {

        [Inject]
        public IDecoyDataService? DecoyDataService { get; set; }

        [Inject]
        public IPredatorDataService? PredatorDataService { get; set; }

        [Inject]
        public IChatlogDataService? ChatlogDataService { get; set; }

        protected bool busy;
        public Predator? predator { get; set; }
        public Decoy? decoy { get; set; }
        public ChatSession? chatsession {get;set;}

        [Parameter]
        public Guid? PredatorId { get; set; }
        [Parameter]
        public Guid? DecoyId { get; set; }
        [Parameter]
        public Guid? ChatSessionId { get; set; }


        protected async Task DeleteEntity()
        {
            var success = 0;
            busy = true;
            if(PredatorId != null)
            {
                success = await PredatorDataService.DeletePredator(PredatorId.Value);
                
            }
            else if (DecoyId != null)
            {
                success = await DecoyDataService.DeleteDecoy(DecoyId.Value);
            }
            else if (ChatSessionId != null)
            {
               success = await ChatlogDataService.DeleteChatSession(ChatSessionId.Value);
            }

            if(success > 0)
            {
                busy = false;
                var message = new NotificationMessage { Style = "position: fixed; top: 0; right: 0", Severity = NotificationSeverity.Success, Summary = "Success", Detail = "Successfully deleted", Duration = 5000 };
                NotificationService.Notify(message);
            }
            else
            {
                var message = new NotificationMessage { Style = "position: fixed; top: 0; right: 0", Severity = NotificationSeverity.Error, Summary = "Failure", Detail = "Failed to delete", Duration = 5000 };
                NotificationService.Notify(message);
            }


            dialogService.Close();
        }
    }
}
