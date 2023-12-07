using System.ComponentModel.DataAnnotations;
using StoreManagement.BL.Domain;

namespace StoreManagement.UI.Web.MVC.Models;

public class NewViewGameModel : IValidatableObject
{
    
    [Required]
    [StringLength(50)]
    public string Name{ get; set; }
    public double? Price{ get; set; } 
    [EnumDataType(typeof(Genre))]
    public Genre Genre{ get; set; }
    public DateOnly YearReleased{ get; set; }
    
    [Range(0,10)]
    public int Rating{ get; set; }
    
    IEnumerable<ValidationResult> IValidatableObject.Validate(ValidationContext validationContext)
    {
        List<ValidationResult> errors = new List<ValidationResult>();
        if (YearReleased > DateOnly.FromDateTime(DateTime.Now))
        {
            ValidationResult error = new ValidationResult("date mag niet in de toekomst liggen", new string[]{"YearReleased"});
            errors.Add(error);
        }
        
        
        return errors;

    }
}