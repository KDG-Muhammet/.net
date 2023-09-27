namespace StoreManagment;

public class Store
{
    private string Name;
    private Game Games;
    private string Address;
    private DateTime OpeningHour;
    
    
    public string ToString()
    {
        return Name + Games + Address + OpeningHour;
    }
}