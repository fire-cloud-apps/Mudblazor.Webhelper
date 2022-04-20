using System.Net.Http.Json;
using System.Text.Json;
using System.Text.Json.Serialization;
using MudBlazor;
using MudBlazorTemplates.WebHelper.Pages.FormValidation;

namespace MudBlazorTemplates.WebHelper.Pages.DataView;

public partial class ServerPaging
{
    
    private IEnumerable<User> pagedData;
    private MudTable<User> table;

    private int totalItems;
    private string searchString = null;

    /// <summary>
    /// Here we simulate getting the paged, filtered and ordered data from the server
    /// </summary>
    private async Task<TableData<User>> ServerReload(TableState state)
    {
        IEnumerable<User> data = await httpClient.GetFromJsonAsync<List<User>>("/public/v2/users");
        await Task.Delay(300);
        data = data.Where(element =>
        {
            if (string.IsNullOrWhiteSpace(searchString))
                return true;
            if (element.Email.Contains(searchString, StringComparison.OrdinalIgnoreCase))
                return true;
            if (element.Name.Contains(searchString, StringComparison.OrdinalIgnoreCase))
                return true;
            if ($"{element.Id} {element.Email} {element.Name}".Contains(searchString))
                return true;
            return false;
        }).ToArray();
        totalItems = data.Count();
        switch (state.SortLabel)
        {
            case "id":
                data = data.OrderByDirection(state.SortDirection, o => o.Id);
                break;
            case "email":
                data = data.OrderByDirection(state.SortDirection, o => o.Email);
                break;
            case "name":
                data = data.OrderByDirection(state.SortDirection, o => o.Name);
                break;
            case "status":
                data = data.OrderByDirection(state.SortDirection, o => o.Status);
                break;
            case "gender":
                data = data.OrderByDirection(state.SortDirection, o => o.Gender);
                break;
        }

        pagedData = data.Skip(state.Page * state.PageSize).Take(state.PageSize).ToArray();
        return new TableData<User>() {TotalItems = totalItems, Items = pagedData};
    }

    private void OnSearch(string text)
    {
        searchString = text;
        table.ReloadServerData();
    }
    
    // private IEnumerable<User> Elements = new List<User>();
    // private bool _readOnly;
    // private bool _isCellEditMode;
    // private List<string> _events = new();
    // private bool _editTriggerRowClick;

    protected override async Task OnInitializedAsync()
    {
        //REf: for httpclient ->https://code-maze.com/blazor-webassembly-httpclient/
        //httpClient.BaseAddress = new Uri("https://try.mudblazor.com/");
        // var result = await httpClient.GetFromJsonAsync<List<User>>("/public/v2/users");
        // string jsonData = JsonSerializer.Serialize(result);
        // Console.WriteLine(jsonData);
        //Elements = result;
    }

    // events
    // void StartedEditingItem(User item)
    // {
    //     _events.Insert(0, $"Event = StartedEditingItem, Data = {System.Text.Json.JsonSerializer.Serialize(item)}");
    // }
    //
    // void CancelledEditingItem(User item)
    // {
    //     _events.Insert(0, $"Event = CancelledEditingItem, Data = {System.Text.Json.JsonSerializer.Serialize(item)}");
    // }
    //
    // void CommittedItemChanges(User item)
    // {
    //     _events.Insert(0, $"Event = CommittedItemChanges, Data = {System.Text.Json.JsonSerializer.Serialize(item)}");
    // }
    
    // private string searchString = "";
    // private Customer customer = new Customer();
    // private IEnumerable<Customer>? customers = new List<Customer>();
    //
    // private async Task<IEnumerable<Customer>?> GetCustomers()
    // {
    //     var response = await Http.GetAsync("periodictable");
    //     var content = await response.Content.ReadAsStringAsync();
    //     if (!response.IsSuccessStatusCode)
    //     {
    //         throw new ApplicationException(content);
    //     }
    //
    //     customers = JsonSerializer.Deserialize<List<Customer>>(content,
    //         new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
    //     return customers;
    //     //Http.GetFromJsonAsync("")
    //     //forecasts = await Http.GetFromJsonAsync<WeatherForecast[]>
    //     // customers = customerService.GetCustomers();
    //     // return customers;
    // }
    //
    // private bool Search(Customer customer)
    // {
    //     return true;
    //     // if (string.IsNullOrWhiteSpace(searchString)) return true;
    //     // if (customer.FirstName.Contains(searchString, StringComparison.OrdinalIgnoreCase)
    //     //     || customer.LastName.Contains(searchString, StringComparison.OrdinalIgnoreCase)
    //     //     || customer.PhoneNumber.Contains(searchString, StringComparison.OrdinalIgnoreCase))
    //     // {
    //     //     return true;
    //     // }
    //     // return false;
    // }
    //
    // private void Save()
    // {
    //     // customerService.SaveCustomer(customer);
    //     // customer = new Customer();
    //     // snackBar.Add("Customer Saved.", Severity.Success);
    //     // GetCustomers();
    // }
    //
    // private void Edit(int id)
    // {
    //     //customer = customers.FirstOrDefault(c => c.Id == id);
    // }
    //
    // private void Delete(int id)
    // {
    //     // customerService.DeleteCustomer(id);
    //     // snackBar.Add("Customer Deleted.", Severity.Success);
    //     // GetCustomers();
    // }
}


/*
 *
 * https://gorest.co.in/public/v2/users
 * {
    "id": 3989,
    "name": "Dhanesh Adiga",
    "email": "adiga_dhanesh@johnson.name",
    "gender": "male",
    "status": "inactive"
  },
 */

public class User
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Email { get; set; }
    public string Gender { get; set; }
    public string Status { get; set; }
}
// public class Element
// {
//     public string Group { get; set; }
//     public int Position { get; set; }
//     public string Name { get; set; }
//     public int Number { get; set; }
//
//     [JsonPropertyName("small")]
//     public string Sign { get; set; }
//     public double Molar { get; set; }
//     public IList<int> Electrons { get; set; }
//
//     /// <summary>
//     /// Overriding Equals is essential for use with Select and Table because they use HashSets internally
//     /// </summary>
//     public override bool Equals(object obj) => object.Equals(GetHashCode(), obj?.GetHashCode());
//
//     /// <summary>
//     /// Overriding GetHashCode is essential for use with Select and Table because they use HashSets internally
//     /// </summary>
//     public override int GetHashCode() => Name?.GetHashCode() ?? 0;
//
//     public override string ToString() => $"{Sign} - {Name}";
// }

public class Customer
{
    public string group { get; set; }
    public int position { get; set; }
    public string name { get; set; }
    public int number { get; set; }
    public string small { get; set; }
    public double molar { get; set; }
}
/*
 
 {
    "group": "",
    "position": 0,
    "name": "Hydrogen",
    "number": 1,
    "small": "H",
    "molar": 1.00794,
    "electrons": [
      1
    ]
  },
  
 */