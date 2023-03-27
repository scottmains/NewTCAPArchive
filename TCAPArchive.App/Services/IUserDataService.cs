
using TCAPArchive.Shared.Domain;
using TCAPArchive.Shared.ViewModels;


namespace TCAPArchive.App.Services
{
    public interface IUserDataService 
    {
    Task<ApplicationUser> CreateUserAsync(RegisterViewModel user);

    Task<bool> LoginAsync(LoginViewModel user);
    Task LogoutAsync();
    }
}
