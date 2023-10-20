using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace StoreManagement.BL.Domain;

public class Game : IValidatableObject
{
    [Required]
    [StringLength(50)]
    public string Name{ get; set; }
    public double? Price{ get; set; } 
    [EnumDataType(typeof(Genre))]
    public Genre Genre{ get; set; }
    [NotMapped]
    public Company Company{ get; set; }
    [NotMapped]
    public List<Store> Stores{ get; set; }
    public DateOnly YearReleased{ get; set; }
    
    [Range(0,10)]
    public int Rating{ get; set; }
    public int Id { get; set; }

    public Game(string name, double? price, Genre genre, DateOnly yearReleased, int rating)
    {
        Name = name;
        Price = price;
        Genre = genre;
        Stores = new List<Store>();
        YearReleased = yearReleased;
        Rating = rating;
        
    }
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