namespace StoreManagment;

public class Game
{
    public string Name{ get; set; }
    private double? Price{ get; set; } 
    public Genre Genre{ get; set; }
    public Company Company{ get; set; }
    public List<Store> Stores{ get; set; }
    private DateTime YearReleased{ get; set; }
    private int Rating{ get; set; }

    public Game(string name, double? price, Genre genre, DateTime yearReleased, int rating)
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
        return string.Format("Name: {0,-25} | Price: {1,-5:n2} $ | Genre: {2}", Name, Price, Genre);
    }
}