using System.Net.Http.Json;
using System.Text.Json;
using System.Text.Json.Serialization;
using MudBlazor;
using MudBlazorTemplates.WebHelper.Pages.FormValidation;
using MudBlazorTemplates.WebHelper.Model;

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
        Console.WriteLine($"Table State : {JsonSerializer.Serialize(state)}");
        return new TableData<User>() {TotalItems = totalItems, Items = pagedData};
    }

    private void OnSearch(string text)
    {
        searchString = text;
        table.ReloadServerData();
    }
    

    protected override async Task OnInitializedAsync()
    {
        //REf: for httpclient ->https://code-maze.com/blazor-webassembly-httpclient/
        //httpClient.BaseAddress = new Uri("https://try.mudblazor.com/");
        // var result = await httpClient.GetFromJsonAsync<List<User>>("/public/v2/users");
        // string jsonData = JsonSerializer.Serialize(result);
        // Console.WriteLine(jsonData);
        //Elements = result;
    }

}


