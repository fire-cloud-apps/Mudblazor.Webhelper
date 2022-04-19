using System.ComponentModel.DataAnnotations;

namespace MudBlazorTemplates.WebHelper.Pages.FormValidation;

public class ClientAccount
{
    #region Basic

    [Required(ErrorMessage = "First is required.")]
    [StringLength(50, ErrorMessage = "Name length can't be more than 50.")]
    public string FirstName { get; set; } = "Ganesh";
    
    [Required(ErrorMessage = "Last is required.")]
    [StringLength(50, ErrorMessage = "Name length can't be more than 50.")]
    public string LastName { get; set; } = "Ram";
    
    [Required(ErrorMessage = "Password is required.")]
    [StringLength(8, ErrorMessage = "Password length can't be more than 8.")]
    public string Password { get; set; } = "G@nesh";

    #endregion
    
    public string Description { get; set; } //Multiline Textbox.
    public BusinessType ClientType { get; set; } // Radio Button
    public bool AccountActive { get; set; } = true; //Switch button
    public DateTime? RegisteredDate { get; set; } = DateTime.Now; // Calendar type
    
    public bool IsAgree { get; set; } = false; // Checkbox
    
    [Required(ErrorMessage = "Amount is Required")]
    public double ClientAmount { get; set; } = 100;
    
    [Required(ErrorMessage = "Product is Required")]
    public Product ProductTypes { get; set; }//Selection Box controller
    
    [Required(ErrorMessage = "State is Required")]
    public CountryState ClientState { get; set;}  //Auto Complete
    
    
}

public enum BusinessType
{
    Education,
    Personal,
    Institute
}

public class Product
{
    public int Id { get; set; }
    public string Name { get; set; }
}

public class CountryState
{
    public int Id { get; set; }
    public string Name { get; set; }
}
