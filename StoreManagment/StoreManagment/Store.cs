namespace StoreManagment;

public class Store
{
    private string Name{ get; set; }
    private Game Games{ get; set; }
    private string Address{ get; set; }
    private TimeOnly OpeningHour{ get; set; }

    public Store(string name, Game games, string address, TimeOnly openingHour)
    {
        Name = name;
        Games = games;
        Address = address;
        OpeningHour = openingHour;
    }

    public string ToString()
    {
        return Name + Games + Address + OpeningHour;
    }
}