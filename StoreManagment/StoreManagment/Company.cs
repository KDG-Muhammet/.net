namespace StoreManagment;

public class Company
{
    private string Name;
    private Game Games;
    private string Address;
    private DateTime YearFounded;
    
    public string ToString()
    {
        return Name + Games + Address + YearFounded;
    }
    
}