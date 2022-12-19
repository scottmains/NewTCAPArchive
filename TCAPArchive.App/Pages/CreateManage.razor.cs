using Microsoft.AspNetCore.Components;

namespace TCAPArchive.App.Pages
{
	public partial class CreateManage
	{

        [Inject]
        public NavigationManager NavigationManager { get; set; }


        protected void NavigateToOverview()
        {
            NavigationManager.NavigateTo("/predatorseoverview");
        }
    }
}
