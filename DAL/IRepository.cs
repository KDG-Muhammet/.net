using StoreManagement.BL.Domain;

namespace StoreManagement.DAL;

public interface IRepository
{
    
    public Game ReadGame(int id);
    public IEnumerable<Game> ReadAllGames();
    public IEnumerable<Game> ReadGameOfGenre(Genre genre); 
    public void CreateGame(Game game);
    public IEnumerable<Game> ReadAllGamesWithCompany();

    
    public Store ReadStore(int id);
    public IEnumerable<Store> ReadAllStore();
    public IEnumerable<Store> ReadStoresByStoreNameAndStoreOpeningHour(string name, int? hour);
    public void CreateStore(Store store);
    public IEnumerable<GameStore> ReadAllStoresWithGames ();
}