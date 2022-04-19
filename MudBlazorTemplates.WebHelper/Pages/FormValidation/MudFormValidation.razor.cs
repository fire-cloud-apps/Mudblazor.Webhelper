using System.ComponentModel.DataAnnotations;
using System.Text.Json;
using Microsoft.AspNetCore.Components;
using MudBlazor;

namespace MudBlazorTemplates.WebHelper.Pages.FormValidation;

public partial class MudFormValidation
{
    [Inject] ISnackbar Snackbar { get; set; }
    MudForm form;
    bool success;
    string[] errors = { };
    public ClientAccount _inputMode = new ClientAccount();

    private IEnumerable<string> MaxCharacters(string ch)
    {
        if (!string.IsNullOrEmpty(ch) && 25 < ch?.Length)
            yield return "Max 25 characters";
    }

    private IEnumerable<string> AmountValidation(double value)
    {
        if (value <= 0)
        {
            yield return "Minimum $1 is required.";
        }
        //if (!string.IsNullOrEmpty(value) && 25 < ch?.Length)
            
    }
    public List<Product> _Products = new List<Product>()
    {
        new Product() { Id = 1, Name = "Name 1" },
        new Product() { Id = 2, Name = "Name 2" },
        new Product() { Id = 3, Name = "Name 3" },
    };
    
    public List<CountryState> _countryStates = new List<CountryState>()
    {
        new CountryState() { Id = 1, Name = "Alabama" },
        new CountryState() { Id = 2, Name = "Alaska" },
        new CountryState() { Id = 3, Name = "American Samoa" },
        new CountryState() { Id = 4, Name = "Arizona" },
    };
    private async Task<IEnumerable<CountryState>> ComplexSearchAsync(string value)
    {
        // In real life use an asynchronous function for fetching data from an api.
        await Task.Delay(5);

        // if text is null or empty, show complete list
        if (string.IsNullOrEmpty(value))
        {
            return _countryStates;
        }
        return _countryStates.Where(x => x.Name.Contains(value, StringComparison.InvariantCultureIgnoreCase));
    }

    
    string _outputJson;

    private async Task Submit()
    {
        await form.Validate();

        if (form.IsValid)
        {
            //Do server actions.
            _outputJson = JsonSerializer.Serialize(_inputMode);

            //Success Message
            Snackbar.Configuration.PositionClass = Defaults.Classes.Position.BottomRight;
            Snackbar.Configuration.SnackbarVariant = Variant.Filled;
            //Can also be done as global configuration. Ref:
            //https://mudblazor.com/components/snackbar#7f855ced-a24b-4d17-87fc-caf9396096a5
            Snackbar.Add("Submited!", Severity.Success);
        }
    }

    #region Password Show or Hide

    string PasswordInputIcon = Icons.Material.Filled.VisibilityOff;
    bool isShow;
    InputType PasswordInput = InputType.Password;

    void ButtonTestclick()
    {
        if (isShow)
        {
            isShow = false;
            PasswordInputIcon = Icons.Material.Filled.VisibilityOff;
            PasswordInput = InputType.Password;
        }
        else
        {
            isShow = true;
            PasswordInputIcon = Icons.Material.Filled.Visibility;
            PasswordInput = InputType.Text;
        }
    }


    #endregion
}