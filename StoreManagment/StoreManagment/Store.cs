namespace StoreManagment;

public class Store
{
    public string Name{ get; set; }
    public List<Game> Games{ get; set; }
    private string Address{ get; set; }
    public TimeOnly OpeningHour{ get; set; }

    public Store(string name, string address, TimeOnly openingHour) 
    {
        Name = name;
        Games = new List<Game>();
        Address = address;
        OpeningHour = openingHour;
    }
 
    public override string ToString()
    {
        return string.Format("Name Store: {0,-20} address: {1,-20} OpeningHour: {2}", Name, Address, OpeningHour);
    }
}