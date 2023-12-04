using StoreManagement.BL.Domain;

namespace StoreManagement.DAL;

public class InMemoryRepository : IRepository
{
    public static List<Game> GamesList { get; set; } = new List<Game>();
    public static List<Store> StoresList { get; set; } = new List<Store>();

    public static void Seed()
    {
        Game game1 = new Game("Assassin's Creed Valhalla", 59.99, Genre.Action, new DateOnly(2022, 3, 8), 9);
        Game game2 = new Game("FIFA 21", 49.99, Genre.Action, new DateOnly(2021, 9, 10 ), 8);
        Game game3 = new Game("Cyberpunk 2077", 69.99, Genre.Adventure, new DateOnly(2020, 5, 7), 7);
        Game game4 = new Game("The Witcher 3: Wild Hunt", 39.99, Genre.Horror, new DateOnly(2023, 7, 13), 9);

        game1.Id = 1;
        game2.Id = 2;
        game3.Id = 3;
        game4.Id = 4;
        
        Store store1 = new Store("Steam Store", "123 Main Street", 10);
        Store store2 = new Store("Epic Games Store", "456 Elm Street", 11);
        Store store3 = new Store("Ubisoft Store", "789 Oak Street", 12);
        Store store4 = new Store("GOG Store", "101 Pine Street", 13);

        store1.Id = 1;
        store2.Id = 2;
        store3.Id = 3;
        store4.Id = 4;
        
        store1.Games.Add(game1);
        store1.Games.Add(game2);
        store2.Games.Add(game2);
        store3.Games.Add(game3);
        store4.Games.Add(game4);
        store1.Games.Add(game3);

        game1.Stores.Add(store1);
        game1.Stores.Add(store2);
        game3.Stores.Add(store1);
        game4.Stores.Add(store2);

        GamesList.AddRange(new[] { game1, game2, game3, game4 });
        StoresList.AddRange(new[] { store1, store2, store3, store4 });
    }
    

    public Game ReadGame(int id)
    {
        foreach (Game game in GamesList)
        {
            if (game.Id == id)
            {
                return game;
            }
        }

        return null;
    }

    public IEnumerable<Game> ReadAllGames()
    {
        return GamesList;
    }

    public IEnumerable<Game> ReadGameOfGenre(Genre genre)
    {
        List<Game> gamesOfGenreList = new List<Game>();
        foreach (Game game in GamesList)
        {
            if (genre == game.Genre)
            {
                gamesOfGenreList.Add(game);
            }
        }

        return gamesOfGenreList;
    }

    public void CreateGame(Game game)
    {
        int id = GamesList.Count + 1;
        game.Id = id;
        GamesList.Add(game);
    }

    public IEnumerable<Game> ReadAllGamesWithCompany()
    {
        throw new NotImplementedException();
    }

    public Store ReadStore(int id)
    {
        foreach (Store store in StoresList)
        {
            if (store.Id == id)
            {
                return store;
            }
        }

        return null;
    }

    public IEnumerable<Store> ReadAllStore()
    {
        return StoresList;
    }

    public IEnumerable<Store> ReadStoresByStoreNameAndStoreOpeningHour(string name, int? hour)
    {
        List<Store> filteredStoreList = new List<Store>();
        foreach (Store store in StoresList)
        {
            if (hour == 0 || store.OpeningHour == hour)
            {
                if (string.IsNullOrEmpty(name) || store.Name.Equals(name, StringComparison.OrdinalIgnoreCase))
                {
                    filteredStoreList.Add(store);
                }
            }
        }

        return filteredStoreList;
    }

    public void CreateStore(Store store)
    {
        int id = StoresList.Count + 1;
        store.Id = id;
        StoresList.Add(store);
    }

    public IEnumerable<Store> ReadAllStoresWithGames()
    {
        throw new NotImplementedException();
    }

    public void CreateGameStore(GameStore gameStore)
    {
        throw new NotImplementedException();
    }

    public void DeleteGameStore(int gameId, int storeId)
    {
        throw new NotImplementedException();
    }

    public IEnumerable<Game> ReadGamesOfStore(int gameId)
    {
        throw new NotImplementedException();
    }
}