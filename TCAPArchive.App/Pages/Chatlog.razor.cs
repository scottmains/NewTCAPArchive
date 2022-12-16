using Microsoft.AspNetCore.Components;
using TCAPArchive.App.Services;
using TCAPArchive.Shared.Domain;

namespace TCAPArchive.App.Pages
{
	public partial class Chatlog
	{
		[Inject]
		public IChatlogDataService ChatlogDataService { get; set; }

		[Parameter]
		public Guid PredatorId { get; set; }
		

		protected async override Task OnInitializedAsync()
		{
			Predator = await PredatorDataService.GetPredatorDetails(int.Parse(PredatorId));
			if (Predator.Longitude.HasValue && Employee.Latitude.HasValue)
			{
				MapMarkers = new List<Marker>
			{
				new Marker{Description = $"{Employee.FirstName} {Employee.LastName}",  ShowPopup = false, X = Employee.Longitude.Value, Y = Employee.Latitude.Value}
			};
			}
		}



	}
}
