using Microsoft.AspNetCore.Components;
using Radzen;
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
            Decoys = (await DecoyDataService.GetAllDecoys()).ToList();
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
