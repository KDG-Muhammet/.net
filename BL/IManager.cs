using StoreManagement.BL.Domain;

namespace StoreManagement.BL;

public interface IManager
{
    
    public Game GetGame(int id);
    public IEnumerable<Game> GetAllGames();
    public IEnumerable<Game> GetGameOfGenre(Genre genre); 
    public Game AddGame(string name, double? price, Genre genre, DateOnly yearReleased, int rating); 
    public IEnumerable<Game> GetAllGamesWithCompany();

    
    public Store GetStore(int id);
    public IEnumerable<Store> GetAllStore();
    public IEnumerable<Store> GetStoresByStoreNameAndStoreOpeningHour(string name, int hour);
    public Store AddStore(string name, string address, int openingHour);
    public IEnumerable<Store> GetAllStoresWithGames ();
    
    void AddGameToStore(int storeId, int gameId);
    void RemoveGameFromStore(int storeId, int gameId);
    IEnumerable<Game> GetGamesOfStore(int storeId);
    

}