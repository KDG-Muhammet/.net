using Microsoft.EntityFrameworkCore;
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
    public Game ReadGameWithStores(int gameId)
    {
        return _ctx.Games
            .Include(g => g.Store)
            .ThenInclude(gs => gs.Store)
            .SingleOrDefault(g => g.Id == gameId);        
    }

    public IEnumerable<Game> ReadAllGames()
    {
        return _ctx.Games;
    }

    public IEnumerable<Game> ReadGameOfGenre(Genre genre)
    {
        return _ctx.Games.Where(game => genre == game.Genre);
    }

    public void CreateGame(Game game)
    {
        int id = _ctx.Games.Count() + 1;
        game.Id = id;
        _ctx.Games.Add(game);
        _ctx.SaveChanges();
    }

    public IEnumerable<Game> ReadAllGamesWithCompany()
    {
        return _ctx.Games.Include(game => game.Company);
    }
    
    public Store ReadStore(int id)
    {
        return _ctx.Stores.SingleOrDefault(store => store.Id == id);
    }

    public IEnumerable<Store> ReadAllStore()
    {
        return _ctx.Stores;
    }

    public IEnumerable<Store> ReadStoresByStoreNameAndStoreOpeningHour(string name, int? hour)
    {
        IEnumerable<Store> storesWithMatchingHour = _ctx.Stores.Where(store => hour == 0 || store.OpeningHour == hour); 
        IEnumerable<Store> matchingStores = storesWithMatchingHour
            .Where(store => string.IsNullOrEmpty(name) || store.Name.Equals(name, StringComparison.OrdinalIgnoreCase));
        return matchingStores;

    }
    public void CreateStore(Store store)
    {
        int id = _ctx.Stores.Count() + 1;
        store.Id = id;
        _ctx.Stores.Add(store);
        _ctx.SaveChanges();
        
    }

    public IEnumerable<Store> ReadAllStoresWithGames()
    {
        return _ctx.Stores.Include(gs => gs.Game).ThenInclude(store => store.Game);
    }

    public void CreateGameStore(GameStore gameStore)
    {
        _ctx.GameStores.Add(gameStore);
        _ctx.SaveChanges();
    }

    public void DeleteGameStore(int gameId, int storeId)
    {
        GameStore gameStore = _ctx.GameStores
            .SingleOrDefault(gs => gs.Game.Id == gameId && gs.Store.Id == storeId);

        if (gameStore != null)
        {
            _ctx.GameStores.Remove(gameStore);
            _ctx.SaveChanges();
        }
    }

    public IEnumerable<Game> ReadGamesOfStore(int storeId)
    {
        return _ctx.GameStores
            .Where(gs => gs.Store.Id == storeId)
            .Select(gs => gs.Game);
    }

    public IEnumerable<Company> ReadAllCompanies()
    {
        return _ctx.Companies;
    }

    public void CreateCompany(Company company)
    {
        _ctx.Companies.Add(company);
        _ctx.SaveChanges();
    }

    public Company ReadCompany(int id)
    {
        return _ctx.Companies.Single(company => company.Id == id);
    }
}