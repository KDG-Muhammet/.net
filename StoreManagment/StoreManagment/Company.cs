namespace StoreManagment;

public class Company
{
    public string Name { get; set; }
    private Game Game{ get; set; }
    private string Address{ get; set; }
    private DateTime YearFounded{ get; set; }
    
    public override string ToString()
    {
        return string.Format("Name Company: {0,-20} address: {1,-20} YearFounded: {2}", Name, Address, YearFounded);

    }
    
}