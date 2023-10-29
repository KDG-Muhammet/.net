using StoreManagement.BL.Domain;

namespace StoreManagement.DAL.EF;

public static class DataSeeder
{

    public static void Seed(GameDbContext gameDbContext)
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
        
        gameDbContext.Games.AddRange(new[] { game1, game2, game3, game4 });
        gameDbContext.Stores.AddRange(new[] { store1, store2, store3, store4 });

        gameDbContext.SaveChanges();
        gameDbContext.ChangeTracker.Clear();


    }
    
}