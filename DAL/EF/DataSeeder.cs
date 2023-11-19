using StoreManagement.BL.Domain;

namespace StoreManagement.DAL.EF;

public static class DataSeeder
{

    public static void Seed(GameDbContext gameDbContext)
    {
        Game assassinCreed = new Game("Assassin's Creed Valhalla", 59.99, Genre.Action, new DateOnly(2022, 3, 8), 9);
        Game fifa21 = new Game("FIFA 21", 49.99, Genre.Action, new DateOnly(2021, 9, 10 ), 8);
        Game cyberpunk2077 = new Game("Cyberpunk 2077", 69.99, Genre.Adventure, new DateOnly(2020, 5, 7), 7);
        Game theWitcher3 = new Game("The Witcher 3: Wild Hunt", 39.99, Genre.Horror, new DateOnly(2023, 7, 13), 9);
        
        Store steam = new Store("Steam Store", "123 Main Street", 10);
        Store epicGames = new Store("Epic Games Store", "456 Elm Street", 11);
        Store ubisoft = new Store("Ubisoft Store", "789 Oak Street", 12);
        Store gog = new Store("GOG Store", "101 Pine Street", 13);
        
        steam.Games.Add(assassinCreed);
        steam.Games.Add(fifa21);
        steam.Games.Add(cyberpunk2077);
        epicGames.Games.Add(fifa21);
        ubisoft.Games.Add(cyberpunk2077);
        gog.Games.Add(theWitcher3);
        
        assassinCreed.Stores.Add(steam);
        cyberpunk2077.Stores.Add(steam);
        cyberpunk2077.Stores.Add(ubisoft);
        theWitcher3.Stores.Add(gog);
        fifa21.Stores.Add(steam);
        fifa21.Stores.Add(epicGames);
        
        gameDbContext.Games.AddRange(new[] { assassinCreed, fifa21, cyberpunk2077, theWitcher3 });
        gameDbContext.Stores.AddRange(new[] { steam, epicGames, ubisoft, gog });

        gameDbContext.SaveChanges();
        gameDbContext.ChangeTracker.Clear();


    }
    
}