namespace StoreManagment;

public class Company
{
    public string Name { get; set; }
    private Game Games{ get; set; }
    private string Address{ get; set; }
    private DateTime YearFounded{ get; set; }
    
    public string ToString()
    {
        return Name + Games + Address + YearFounded;
    }
    
}