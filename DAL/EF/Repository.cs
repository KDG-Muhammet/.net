using StoreManagement.BL.Domain;

namespace StoreManagement.DAL.EF;

public class Repository : IRepository
{
    
    private readonly GameDbContext _ctx;

    public Repository(GameDbContext ctx)
    {
        _ctx = ctx;
    }

    public Game ReadGame(int id)
    {
        throw new NotImplementedException();
    }

    public IEnumerable<Game> ReadAllGames()
    {
        return _ctx.Games;
    }

    public IEnumerable<Game> ReadGameOfGenre(int genre)
    {
        throw new NotImplementedException();
    }

    public void CreateGame(Game game)
    {
        throw new NotImplementedException();
    }

    public Store ReadStore(int id)
    {
        throw new NotImplementedException();
    }

    public IEnumerable<Store> ReadAllStore()
    {
        return _ctx.Stores;
    }

    public IEnumerable<Store> ReadStoresByGameNameAndStoreOpeningHour(string name, int hour)
    {
        throw new NotImplementedException();
    }

    public void CreateStore(Store store)
    {
        throw new NotImplementedException();
    }
}