using Domain;

namespace DAL;

public interface IRepository
{
    
    public Game ReadGame(int id);
    public IEnumerable<Game> ReadAllGames();
    public IEnumerable<Game> ReadGameOfGenre(int genre); 
    public void CreateGame(Game game);
    
    public Store ReadStore(int id);
    public IEnumerable<Store> ReadAllStore();
    public IEnumerable<Store> ReadStoresByGameNameAndStoreOpeningHour(string name, int hour);
    public void CreateStore(Store store);
     
     
}