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


        protected override async Task OnInitializedAsync()
        {
            DialogService.OnOpen += Open;
            DialogService.OnClose += Close;
            Decoys = (await DecoyDataService.GetAllDecoys()).ToList();
        }

        public void Dispose()
        {
            // The DialogService is a singleton so it is advisable to unsubscribe.
            DialogService.OnOpen -= Open;
            DialogService.OnClose -= Close;
        }

        void Open(string title, Type type, Dictionary<string, object> parameters, DialogOptions options)
        {
            
        }

        void Close(dynamic result)
        {
            StateHasChanged();
        }

        public async Task OpenDecoyEdit(Guid decoyId, string decoyHandle)
        {
            await DialogService.OpenAsync<DecoyEdit>($" Edit {decoyHandle}",
                   new Dictionary<string, object>() { { "decoyId", decoyId } },
                   new DialogOptions() { Width = "700px", Height = "512px", Resizable = true, Draggable = true });
        }

        public async Task OpenDecoyCreate()
        {
            await DialogService.OpenAsync<DecoyCreate>($" Create ",
                   new Dictionary<string, object>() { },
                   new DialogOptions() { Width = "700px", Height = "512px", Resizable = true, Draggable = true });
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
                    await DecoyDataService.DeleteDecoy(decoyId);
                }
                catch (Exception exception)
                {
                    NotificationService.Notify(NotificationSeverity.Error, $"Error",
                        $"Foo", duration: -1);

                }

            }

            StateHasChanged();
        }



    }
}
