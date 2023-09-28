namespace StoreManagment;

public class Game
{
    private string Name{ get; set; }
    private double? Price{ get; set; } 
    private Genre Genre{ get; set; }
    private string Company{ get; set; }
    private string Stores{ get; set; }
    private DateTime YearReleased{ get; set; }
    private int Rating{ get; set; }

    public Game(string name, double? price, Genre genre, string company, string stores, DateTime yearReleased, int rating)
    {
        Name = name;
        Price = price;
        Genre = genre;
        Company = company;
        Stores = stores;
        YearReleased = yearReleased;
        Rating = rating;
    }


    public string ToString()
    {
        return Name + Price + Genre + Company + Stores + YearReleased+ Rating;
    }

}