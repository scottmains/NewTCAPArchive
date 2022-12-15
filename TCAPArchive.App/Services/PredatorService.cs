namespace TCAPArchive.App.Services
{
	public class PredatorService : IPredatorService
	{
		private readonly HttpClient _httpClient;

		public PredatorService(HttpClient httpClient)
		{
			_httpClient = httpClient;
			
		}

		
	}
}
