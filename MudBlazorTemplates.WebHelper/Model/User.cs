namespace MudBlazorTemplates.WebHelper.Model;


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