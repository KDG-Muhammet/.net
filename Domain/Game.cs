namespace Domain;

public class Game
{
    public string Name{ get; set; }
    public double? Price{ get; set; } 
    public Genre Genre{ get; set; }
    public Company Company{ get; set; }
    public List<Store> Stores{ get; set; }
    public DateTime YearReleased{ get; set; }
    public int Rating{ get; set; }
    public int Id { get; set; }

    public Game(string name, double? price, Genre genre, DateTime yearReleased, int rating, int id)
    {
        Name = name;
        Price = price;
        Genre = genre;
        Stores = new List<Store>();
        YearReleased = yearReleased;
        Rating = rating;
        Id = id;

    }
    

    public override string ToString()
    {
        return String.Format("Name: {0,-25} | Price: {1,-5:n2} $ | Genre: {2}", Name, Price, Genre);
    }
}