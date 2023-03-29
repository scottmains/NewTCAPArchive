using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using Radzen;
using System;
using System.Runtime.InteropServices;
using TCAPArchive.App.Components;
using TCAPArchive.App.Components.Create;
using TCAPArchive.App.Components.Edit;
using TCAPArchive.App.Services;
using TCAPArchive.Shared.Domain;

namespace TCAPArchive.App.Pages
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

        public async Task OpenDeleteDialog(Guid decoyId, string decoyName)
        {
            await DialogService.OpenAsync<DeleteDialog>($" Delete {decoyName}",
                   new Dictionary<string, object>() { { "decoyId", decoyId } },
                   new DialogOptions() { Width = "500px", Height = "200px", Resizable = true, Draggable = true });

            await RefreshData();

        }
    }
}
