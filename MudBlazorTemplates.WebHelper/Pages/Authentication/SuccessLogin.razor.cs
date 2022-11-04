using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.WebUtilities;
using MudBlazorTemplates.WebHelper.Helper;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using MudBlazorTemplates.WebHelper.Model;
using System.Text.Json;
using System.Text.Json.Serialization;
using MudBlazorTemplates.WebHelper.Service;

namespace MudBlazorTemplates.WebHelper.Pages.Authentication;

public partial class SuccessLogin
{
    private string _jwt;
    private string _message;
    private string _encode;
    List<Claim> _claimList;
    protected  async override void OnInitialized()
    {
        var uri = _navigationManager.ToAbsoluteUri(_navigationManager.Uri);
        
        _navigationManager.TryGetQueryString<string>("jwt", out _jwt);
        _navigationManager.TryGetQueryString<string>("msg", out _message);

        var handler = new JwtSecurityTokenHandler();
        var token = handler.ReadJwtToken(_jwt);

        _claimList = token.Claims.ToList();

        User user = new User();
        foreach (var claim in _claimList)
        {
            switch (claim.Type)
            {
                case "Id":
                    user.Id = claim.Value;
                    break;
                case "Name":
                    user.Name = claim.Value;
                    break;
                case "Email":
                    user.Email = claim.Value;
                    break;
                case "AccountId":
                    user.AccountId = claim.Value;
                    break;
                case "ConnectionKey":
                    user.ConnectionKey = claim.Value;
                    break;
                case "Picture":
                    user.Picture = claim.Value;
                    break;
                default:
                    break;
            }
        }

        string jsonString = JsonSerializer.Serialize(user);
        Console.WriteLine(jsonString);

        await _localStore.SetItem("user", user);

        var users = await _userService.GetAll();

        if (users != null)
        {
            _navigationManager.NavigateTo("Counter");
        }
        else
        {
            _navigationManager.NavigateTo("Error?code=500");
        }

    }
}