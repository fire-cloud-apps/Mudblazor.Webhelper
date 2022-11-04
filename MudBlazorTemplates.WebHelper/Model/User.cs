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
    public string Id { get; set; }
    public string Name { get; set; }
    public string Email { get; set; }
    public string Gender { get; set; }
    public string Status { get; set; }
    
    public string FirstName { get; set; }
    
    public string LastName { get; set; }
    
    public string Username { get; set; }
    public string Password { get; set; }
    public string DomainURL { get; set; }
    public string UserType { get; set; }
    public string JwtToken { get; set; }
    
    public string AccountId { get; set; }
    public string ConnectionKey { get; set; }

    public string Picture { get; set; }
}

public enum UserCategory
{
    ClientUser,
    FcUser 
}