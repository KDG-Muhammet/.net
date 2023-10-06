namespace StoreManagement;

public class Store
{
    public string Name{ get; set; }
    public List<Game> Games{ get; set; }
    public string Address{ get; set; }
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
        return String.Format("Name Store: {0,-20} address: {1,-20} OpeningHour: {2}", Name, Address, OpeningHour);
    }
}