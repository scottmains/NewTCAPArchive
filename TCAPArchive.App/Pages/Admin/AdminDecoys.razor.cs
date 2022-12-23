using Microsoft.AspNetCore.Components;
using Radzen;
using System;
using System.Runtime.InteropServices;
using TCAPArchive.App.Components.Admin;
using TCAPArchive.App.Services;
using TCAPArchive.Shared.Domain;

namespace TCAPArchive.App.Pages.Admin
{
    public partial class AdminDecoys
    {
        [Inject]
        public IDecoyDataService? DecoyDataService { get; set; }
        public List<Decoy> Decoys { get; set; }
        public Decoy decoy { get; set; }
        protected string Message = string.Empty;
        protected string StatusClass = string.Empty;
        protected bool Saved;

        protected override async Task OnInitializedAsync()
        {
        
            Decoys = (await DecoyDataService.GetAllDecoys()).ToList();
        }

  
    
        public async Task OpenDecoyEdit(Guid decoyId, string decoyHandle)
        {
            var result = await DialogService.OpenAsync<DecoyEdit>($" Edit {decoyHandle}",
                   new Dictionary<string, object>() { { "decoyId", decoyId } },
                   new DialogOptions() { Width = "700px", Height = "512px", Resizable = true, Draggable = true });

        
                await RefreshData();
        }

        public async Task OpenDecoyCreate()
        {
           var result = await DialogService.OpenAsync<DecoyCreate>($" Create ",
                   new Dictionary<string, object>() { },
                   new DialogOptions() { Width = "700px", Height = "512px", Resizable = true, Draggable = true });

         
                await RefreshData();
            
       
        }

        public async Task RefreshData()
        {
            Decoys = (await DecoyDataService.GetAllDecoys()).ToList();
        }

        public async Task DeleteButtonClick(Guid decoyId)
        {
            // Ask for confirmation:
            var confirmResult = await DialogService.Confirm(
                "Are you sure?", "Delete decoy");

            if (confirmResult.HasValue && confirmResult.Value)
            {
                try
                {
                   var success = await DecoyDataService.DeleteDecoy(decoyId);

                    if (success > 0)
                    {
                        StatusClass = "alert-success";
                        Saved = true;
                    }

                    if (Saved)
                    {
                        var message = new NotificationMessage { Style = "position: fixed; top: 0; right: 0", Severity = NotificationSeverity.Success, Summary = "Success", Detail = "Succesfully deleted decoy", Duration = 5000 };
                        NotificationService.Notify(message);
                    }
                    else
                    {
                        var message = new NotificationMessage { Style = "position: fixed; top: 0; right: 0", Severity = NotificationSeverity.Error, Summary = "Failure", Detail = "failed to delete decoy", Duration = 5000 };
                        NotificationService.Notify(message);
                    }


                    await RefreshData();
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
