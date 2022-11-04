using Microsoft.AspNetCore.Components;
using System.Threading.Tasks;
using FC.Core.Extension.StringHandlers;
using MudBlazorTemplates.WebHelper.Model;

namespace MudBlazorTemplates.WebHelper.Service;
public interface IAuthenticationService
{
    User User { get; }
    Task Initialize();
    Task<User> Login(User user);
    Task Logout();
}

public class AuthenticationService : IAuthenticationService
{
    private IHttpService _httpService;
    private NavigationManager _navigationManager;
    private ILocalStorageService _localStorageService;
    

    public User User { get; private set; }

    public AuthenticationService(
        IHttpService httpService,
        NavigationManager navigationManager,
        ILocalStorageService localStorageService
    ) {
        _httpService = httpService;
        _navigationManager = navigationManager;
        _localStorageService = localStorageService;
    }

    public async Task Initialize()
    {
        User = await _localStorageService.GetItem<User>("user");
    }
    
    public async Task<User> Login(User user)
    {
        //Console.WriteLine(user.ToJSON());
        User = await _httpService.Post<User>("/API/UserAuth/FCAuth", 
            new
            {
                user.Username, 
                user.Password, 
                user.DomainURL,
                user.UserType
            });
        await _localStorageService.SetItem("user", User);
        return user;
    }

    public async Task Logout()
    {
        User = null;
        await _localStorageService.RemoveItem("user");
        _navigationManager.NavigateTo("login");
    }
}