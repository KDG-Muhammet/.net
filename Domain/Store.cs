using System.ComponentModel.DataAnnotations;

namespace Domain;

public class Store
{
    [Required]
    public string Name{ get; set; }
    public List<Game> Games{ get; set; }
    public string Address{ get; set; }
    [Range(0,24)]
    public TimeOnly OpeningHour{ get; set; }
    public int Id { get; set; }
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