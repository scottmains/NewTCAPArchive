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
			
			}
		}



	}

