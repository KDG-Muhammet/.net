using System.Globalization;
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
        return _ctx.Games.Single(game => game.Id == id);
    }

    public IEnumerable<Game> ReadAllGames()
    {
        return _ctx.Games;
    }

    public IEnumerable<Game> ReadGameOfGenre(int genre)
    {
        return _ctx.Games.Where(game => genre == (int)game.Genre);
    }

    public void CreateGame(Game game)
    {
        int id = _ctx.Games.Count() + 1;
        game.Id = id;
        _ctx.Games.Add(game);
        _ctx.SaveChanges();
    }

    public Store ReadStore(int id)
    {
        return _ctx.Stores.Single(store => store.Id == id);
    }

    public IEnumerable<Store> ReadAllStore()
    {
        return _ctx.Stores;
    }

    public IEnumerable<Store> ReadStoresByGameNameAndStoreOpeningHour(string name, int hour)
    {
        
        IEnumerable<Store> storesWithMatchingHour = _ctx.Stores.Where(store => hour == 0 || store.OpeningHour == hour); 
        IEnumerable<Store> matchingStores = storesWithMatchingHour
            .Where(store => string.IsNullOrEmpty(name) || store.Games.Any(game => game.Name.Equals(name, StringComparison.OrdinalIgnoreCase)));

        return matchingStores;

    }
    public void CreateStore(Store store)
    {
        int id = _ctx.Stores.Count() + 1;
        store.Id = id;
        _ctx.Stores.Add(store);
        _ctx.SaveChanges();
        
    }
}