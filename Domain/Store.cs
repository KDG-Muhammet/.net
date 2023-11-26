using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace StoreManagement.BL.Domain;

public class Store
{
    [Required]
    public string Name{ get; set; }
    public List<Game> Games{ get; set; }
    public string Address{ get; set; }
    [Range(1,24)]
    public int OpeningHour{ get; set; }
    public int Id { get; set; }
    public ICollection<GameStore> Game { get; set; }
    public Store(string name, string address, int openingHour) 
    {
        Name = name;
        Games = new List<Game>();
        Address = address;
        OpeningHour = openingHour;
    }
    
}