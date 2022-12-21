﻿using Microsoft.AspNetCore.Components;
using Radzen;
using TCAPArchive.App.Components.Admin;
using TCAPArchive.App.Services;
using TCAPArchive.Shared.Domain;

namespace TCAPArchive.App.Pages.Admin
{
    public partial class AdminPredators
    {
        [Inject]
        public IPredatorDataService? PredatorDataService { get; set; }
        public List<Predator> Predators { get; set; }
        public Predator predator { get; set; }


        protected override async Task OnInitializedAsync()
        {
            Predators = (await PredatorDataService.GetAllPredators()).ToList();
        }

        public async Task OpenPredatorEdit(Guid predatorId, string predatorName)
        {
            await DialogService.OpenAsync<PredatorEdit>($" Edit {predatorName}",
                   new Dictionary<string, object>() { { "predatorId", predatorId } },
                   new DialogOptions() { Width = "700px", Height = "512px", Resizable = true, Draggable = true });
        }

        public async Task OpenPredatorCreate()
        {
            await DialogService.OpenAsync<PredatorCreate>($" Create ",
                   new Dictionary<string, object>() { },
                   new DialogOptions() { Width = "700px", Height = "512px", Resizable = true, Draggable = true });
        }

        public async Task DeleteButtonClick(Guid predatorId)
        {
            // Ask for confirmation:
            var confirmResult = await DialogService.Confirm(
                "Yes", "No");

            if (confirmResult.HasValue && confirmResult.Value)
            {
                try
                {
                    await PredatorDataService.DeletePredator(predatorId);
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
