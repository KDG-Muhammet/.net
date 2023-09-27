namespace StoreManagment;

public class Game
{
    private string Name;
    private double? Price; 
    private Genre Genre;
    private Company Company;
    private Store Stores;
    private DateTime YearReleased;
    private int Rating;
    
    
    public string ToString()
    {
        return Name + Price + Genre + Company + Stores + YearReleased+ Rating;
    }

}