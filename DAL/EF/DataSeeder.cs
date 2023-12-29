using StoreManagement.BL.Domain;

namespace StoreManagement.DAL.EF;

public static class DataSeeder
{

    public static void Seed(GameDbContext gameDbContext)
    {
        Game assassinCreed = new Game("Assassin's Creed Valhalla", 59.99, Genre.Action, new DateOnly(2022, 3, 8), 9);
        Game fifa21 = new Game("FIFA 21", null, Genre.Action, new DateOnly(2021, 9, 10 ), 8);
        Game cyberpunk2077 = new Game("Cyberpunk 2077", 69.99, Genre.Adventure, new DateOnly(2020, 5, 7), 7);
        Game theWitcher3 = new Game("The Witcher 3: Wild Hunt", 39.99, Genre.Horror, new DateOnly(2023, 7, 13), 9);
        
        Store steam = new Store("Steam Store", "123 Main Street", 10);
        Store epicGames = new Store("Epic Games Store", "456 Elm Street", 11);
        Store ubisoftStore = new Store("Ubisoft Store", "789 Oak Street", 12);
        Store gog = new Store("GOG Store", "101 Pine Street", 13);
        
        Company ea = new Company("EA", "Redwood City, California, US", new DateOnly(1982, 5, 27));
        Company sony = new Company("Sony", "Kōnan, Minato, Tokyo, Japan", new DateOnly(1946, 5, 	7));
        Company cDProjekt = new Company("CD Projekt", "Warsaw, Poland", new DateOnly(1994, 5, 1));
        Company ubisoft = new Company("Ubisoft", "Saint-Mandé, France", new DateOnly(1986, 3, 28));
        Company microsoft = new Company("Microsoft", "Redmond, Washington, U.S.", new DateOnly(1975, 4, 4));

        GameStore gameStore1 = new GameStore() { Game = fifa21 , Store = steam, Sales = 100.0 };
        GameStore gameStore2 = new GameStore() { Game = assassinCreed , Store = steam, Sales = 150.0 };
        GameStore gameStore3 = new GameStore() { Game = cyberpunk2077 , Store = steam, Sales = 120.0 };
        GameStore gameStore4 = new GameStore() { Game = fifa21 , Store = epicGames, Sales = 200.0 };
        GameStore gameStore5 = new GameStore() { Game = cyberpunk2077 , Store = ubisoftStore, Sales = 80.0 };
        GameStore gameStore6 = new GameStore() { Game = theWitcher3 , Store = gog, Sales = 110.0};

        
        // steam.Games.Add(assassinCreed);
        // steam.Games.Add(fifa21);
        // steam.Games.Add(cyberpunk2077);
        // epicGames.Games.Add(fifa21);
        // ubisoftStore.Games.Add(cyberpunk2077);
        // gog.Games.Add(theWitcher3);
        //
        // assassinCreed.Stores.Add(steam);
        // cyberpunk2077.Stores.Add(steam);
        // cyberpunk2077.Stores.Add(ubisoftStore);
        // theWitcher3.Stores.Add(gog);
        // fifa21.Stores.Add(steam);
        // fifa21.Stores.Add(epicGames);

        fifa21.Company = ea;
        assassinCreed.Company = ubisoft;
        cyberpunk2077.Company = cDProjekt;
        theWitcher3.Company = cDProjekt;

        ea.Games.Add(fifa21);
        ubisoft.Games.Add(assassinCreed);
        cDProjekt.Games.Add(cyberpunk2077);
        cDProjekt.Games.Add(theWitcher3);

        steam.Game= new List<GameStore>() { gameStore1, gameStore2, gameStore3 };
        epicGames.Game = new List<GameStore>() { gameStore4 };
        ubisoftStore.Game = new List<GameStore>() { gameStore5 };
        gog.Game = new List<GameStore>() { gameStore6 };
        
        
        // gameDbContext.Games.AddRange(new[] { assassinCreed, fifa21, cyberpunk2077, theWitcher3 });
        // gameDbContext.Stores.AddRange(new[] { steam, epicGames, ubisoftStore, gog });
        gameDbContext.GameStores.AddRange(new []{ gameStore1, gameStore2, gameStore3,gameStore4,gameStore5,gameStore6});
        gameDbContext.Companies.AddRange(new []{ ea, sony, cDProjekt,ubisoft,microsoft});


        gameDbContext.SaveChanges();
        gameDbContext.ChangeTracker.Clear();


    }
    
}