using Domain;

namespace BL;

public interface IManager
{
    
    public Game GetGame(int id);
    public IEnumerable<Game> GetAllGames();
    public IEnumerable<Game> GetGameOfGenre(int genre); 
    public Game AddGame(string name, double? price, Genre genre, DateTime yearReleased, int rating); 
    
    public Store GetStore(int id);
    public IEnumerable<Store> GetAllStore();
    public IEnumerable<Store> GetStoresByGameNameAndStoreOpeningHour(string name, int hour);
    public Store AddStore(string name, string address, TimeOnly openingHour);
    
    
}