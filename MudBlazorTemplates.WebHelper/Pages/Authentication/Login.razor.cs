using System.ComponentModel.DataAnnotations;
using FC.Core.Extension.StringHandlers;
using MudBlazor;
using MudBlazorTemplates.WebHelper.Helper;
using MudBlazorTemplates.WebHelper.Model;

namespace MudBlazorTemplates.WebHelper.Pages.Authentication;

public partial class Login
{
    string Password { get; set; } = "SamplePassword";

    bool PasswordVisibility;
    InputType PasswordInput = InputType.Password;
    string PasswordInputIcon = Icons.Material.Filled.VisibilityOff;
    private Model model = new Model();
    private bool loading;
    private string error;
    /// <summary>
    /// Current Domain URL 
    /// </summary>
    private string _domainURL = string.Empty;

    protected override void OnInitialized()
    {
        _domainURL = _navigationManager.BaseUri;
        //redirect to home if already logged in
        if (AuthenticationService.User != null)
        {
            NavigationManager.NavigateTo("Counter");
            //NavigationManager.NavigateTo("/personal/dashboard");
        }
    }

    private class Model
    {
        [Required]
        public string Username { get; set; }

        [Required]
        public string Password { get; set; }
    }
    private async Task LoginInvoke()
    {
        //var oAuth = _configuration.GetSection("App:OAuthURL");
        //Console.WriteLine(oAuth.ToJSON());
        try
        {
            User request = new User()
            {
                Username = model.Username,
                Password = model.Password,
                DomainURL = NavigationManager.BaseUri,
                UserType = UserCategory.FcUser.ToString(),//As JSON is not able to detect this enum value.
            };
            User user = await AuthenticationService.Login(request);
            var returnUrl = NavigationManager.QueryString("returnUrl") ?? "";
            NavigationManager.NavigateTo($"/index?{returnUrl}");
            Snackbar.Add($"Welcome {user.Username} {user.LastName}!", Severity.Success);
        }
        catch (Exception ex)
        {
            error = ex.Message;
            Snackbar.Configuration.PositionClass = Defaults.Classes.Position.BottomCenter;
            Snackbar.Add(ex.Message, Severity.Error);
            loading = false;
            StateHasChanged();
        }
    }

    private Task LoginGoogle()
    {
        //var querystring = NavigationManager.QueryString();
        Console.WriteLine(_configuration["App:GoogleAuth"]);
        NavigationManager.NavigateTo(_configuration["App:GoogleAuth"] + _domainURL);
        Console.WriteLine(_domainURL);
        return Task.CompletedTask;
    }

    void TogglePasswordVisibility()
    {
        if (PasswordVisibility)
        {
            PasswordVisibility = false;
            PasswordInputIcon = Icons.Material.Filled.VisibilityOff;
            PasswordInput = InputType.Password;
        }
        else
        {
            PasswordVisibility = true;
            PasswordInputIcon = Icons.Material.Filled.Visibility;
            PasswordInput = InputType.Text;
        }
    }
}