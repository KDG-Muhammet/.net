namespace StoreManagement.BL.Domain; 

public class Company
{
    public string Name { get; set; }
    public List<Game> Games{ get; set; }
    public string Address{ get; set; }
    public DateTime YearFounded{ get; set; }
    
    public int Id { get; set; }
    

    public Company(string name, string address, DateTime yearFounded)
    {
        Name = name;
        Games = new List<Game>();
        Address = address;
        YearFounded = yearFounded;
    }
    
}