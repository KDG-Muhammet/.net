using StoreManagement.BL.Domain;

namespace StoreManagement.DAL;

public interface IRepository
{
    
    public Game ReadGame(int id);
    public IEnumerable<Game> ReadAllGames();
    public IEnumerable<Game> ReadGameOfGenre(Genre genre); 
    public void CreateGame(Game game);
    public IEnumerable<Game> ReadAllGamesWithCompany();
    public Game ReadGameWithStores(int id);
    public void UpdateRating(Game game);
    
    public Store ReadStore(int id);
    public IEnumerable<Store> ReadAllStore();
    public IEnumerable<Store> ReadStoresByStoreNameAndStoreOpeningHour(string name, int? hour);
    public void CreateStore(Store store);
    public IEnumerable<Store> ReadAllStoresWithGames ();
    
    void CreateGameStore(GameStore gameStore);
    void DeleteGameStore(int gameId, int storeId);
    IEnumerable<Game> ReadGamesOfStore(int storeId);
    
   public IEnumerable<Company> ReadAllCompanies();
   void CreateCompany(Company company);
   public Company ReadCompany(int id);
   
}