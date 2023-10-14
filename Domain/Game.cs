using System.ComponentModel.DataAnnotations;

namespace Domain;

public class Game : IValidatableObject
{
    [Required]
    [StringLength(50)]
    public string Name{ get; set; }
    public double? Price{ get; set; } 
    [EnumDataType(typeof(Genre))]
    public Genre Genre{ get; set; }
    public Company Company{ get; set; }
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
    
    public override string ToString()
    {
        return String.Format("Name: {0,-25} | Price: {1,-5:n2} $ | Genre: {2}", Name, Price, Genre);
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