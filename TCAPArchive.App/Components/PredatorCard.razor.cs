using Microsoft.AspNetCore.Components;
using TCAPArchive.App.Services;
using TCAPArchive.Shared.Domain;

namespace TCAPArchive.App.Components
{
    public partial class PredatorCard
    {
        [Parameter]
        public Predator predator { get; set; } = default!;
     

        public string PredatorImage { get; set; }

        protected override async Task OnInitializedAsync()
        {
            string predatorImage = Convert.ToBase64String(predator.ImageData);
       
            PredatorImage = string.Format("data:image/jpg;base64,{0}", predatorImage);
        }
    }
}
