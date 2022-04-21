using System.Net.Http.Json;
using System.Text.Json;

namespace MudBlazorTemplates.WebHelper.Pages.DataView;
using MudBlazorTemplates.WebHelper.Model;

public partial class InlineEdit
{
    private List<string> editEvents = new();
    private bool dense = false;
    private bool hover = true;
    private bool ronly = false;
    private bool canCancelEdit = false;
    private bool blockSwitch = false;
    private string searchString = "";
    private User selectedItem1 = null;
    private User UserBeforeEdit;
    private HashSet<User> selectedItems1 = new HashSet<User>();
//@(Model.address + " " + Model.city)
    private IEnumerable<User> Users = new List<User>();

    protected override async Task OnInitializedAsync()
    {
        Users = await httpClient.GetFromJsonAsync<List<User>>("/public/v2/users");
    }

    private void ClearEventLog()
    {
        editEvents.Clear();
    }

    private void AddEditionEvent(string message)
    {
        editEvents.Add(message);
        StateHasChanged();
    }

    private void BackupItem(object user)
    {
        UserBeforeEdit = new()
        {
            Email = ((User)user).Email,
            Name = ((User)user).Name,
            Gender = ((User)user).Gender,
            Status = ((User)user).Status
        };
        AddEditionEvent($"RowEditPreview event: made a backup of User {((User)user).Name}");
    }

    private void ItemHasBeenCommitted(object user)
    {
        User? usr = user as User;
        Console.WriteLine($"User Edited Value : {JsonSerializer.Serialize(usr)}");
        AddEditionEvent($"RowEditCommit event: Changes to User {((User)user).Name} committed");
    }

    private void ResetItemToOriginalValues(object user)
    {
        ((User)user).Email = UserBeforeEdit.Email;
        ((User)user).Name = UserBeforeEdit.Name;
        ((User)user).Gender = UserBeforeEdit.Gender;
        ((User)user).Status = UserBeforeEdit.Status;
        AddEditionEvent($"RowEditCancel event: Editing of User {((User)user).Name} cancelled");
    }

    private bool FilterFunc(User user)
    {
        if (string.IsNullOrWhiteSpace(searchString))
            return true;
        if (user.Email.Contains(searchString, StringComparison.OrdinalIgnoreCase))
            return true;
        if (user.Name.Contains(searchString, StringComparison.OrdinalIgnoreCase))
            return true;
        if ($"{user.Id} {user.Gender} {user.Status}".Contains(searchString))
            return true;
        return false;
    } 
}