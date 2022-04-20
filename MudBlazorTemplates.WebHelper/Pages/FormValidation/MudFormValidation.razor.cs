using System.ComponentModel.DataAnnotations;
using System.Text.Json;
using Microsoft.AspNetCore.Components;
using MudBlazor;

namespace MudBlazorTemplates.WebHelper.Pages.FormValidation;

public partial class MudFormValidation
{
    #region Initialization
    [Inject] ISnackbar Snackbar { get; set; }
    MudForm form;
    bool success;
    string[] errors = { };
    public ClientAccount _inputMode = new ClientAccount();
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
    #endregion

    private bool _loading = false;
    protected override async Task OnInitializedAsync()
    {
        _loading = true;
        await  Task.Delay(2000);
        _loading = false;
        
        StateHasChanged();
        Console.WriteLine("Render Completed.");
        _inputMode.ProductTypes = _Products[2];//Binding to Selection control
        _inputMode.ClientState = _countryStates[3];//Binding to Autocomplete
        StateHasChanged();
    }
    
    private IEnumerable<string> MaxCharacters(string ch)
    {
        if (!string.IsNullOrEmpty(ch) && 25 < ch?.Length)
            yield return "Max 25 characters";
    }

    private MudTextField<string> txtDescription;
    private IEnumerable<string> Max300Characters(string ch)
    {
        if (!string.IsNullOrEmpty(ch) && 300 < ch?.Length)
            yield return $"Max 300 characters";
    }

    /// <summary>
    /// Custom validation
    /// </summary>
    /// <param name="value">double value</param>
    /// <returns>Error message</returns>
    private IEnumerable<string> AmountValidation(double value)
    {
        if (value <= 0)
        {
            yield return "Minimum $1 is required.";
        }
        //if (!string.IsNullOrEmpty(value) && 25 < ch?.Length)
            
    }
   
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


    #region Submit Button with Animation
    string _outputJson;
    private bool _processing = false;
    async Task ProcessSomething()
    {
        _processing = true;
        await Task.Delay(2000);
        _processing = false;
    }
    private async Task Submit()
    {
        await form.Validate();

        if (form.IsValid)
        {
            // //Todo some animation.
            await ProcessSomething();
            
            //Do server actions.
            _outputJson = JsonSerializer.Serialize(_inputMode);

            //Success Message
            Snackbar.Configuration.PositionClass = Defaults.Classes.Position.BottomRight;
            Snackbar.Configuration.SnackbarVariant = Variant.Filled;
            //Snackbar.Configuration.VisibleStateDuration  = 2000;
            //Can also be done as global configuration. Ref:
            //https://mudblazor.com/components/snackbar#7f855ced-a24b-4d17-87fc-caf9396096a5
            Snackbar.Add("Submited!", Severity.Success);
        }
        else
        {
            _outputJson = "Validation Error occured.";
            Console.WriteLine(_outputJson);
        }
    }
    #endregion
    
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