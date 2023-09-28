using System.Net.Sockets;

namespace StoreManagment;
public class ConsoleUi
{
    
    
    private List<Game> Games = new List<Game>();
    private List<Store> Stores = new List<Store>();

    public void Seed()
    {
        Game game1 = new Game("Assassin's Creed Valhalla", 59.99, Genre.actionGenre, "ubisoft", "steam",
            new DateTime(2022, 3, 8, 10, 0, 0), 9);
        Game game2 = new Game("FIFA 21", 49.99, Genre.actionGenre, "ea", "steam", new DateTime(2021, 9, 10, 10, 0, 0),
            8);
        Game game3 = new Game("Cyberpunk 2077", 69.99, Genre.actionGenre, "cdProjektRed", "epicGamesStore",
            new DateTime(2020, 5, 7, 10, 0, 0), 7);
        Game game4 = new Game("The Witcher 3: Wild Hunt", 39.99, Genre.adventureGenre, "cdProjektRed", "steam",
            new DateTime(2023, 7, 13, 10, 0, 0), 9);

        Store store1 = new Store("Steam Store", game1, "123 Main Street", new TimeOnly(10, 0, 0));
        Store store2 = new Store("Epic Games Store", game2, "456 Elm Street", new TimeOnly(11, 0, 0));
        Store store3 = new Store("Ubisoft Store", game3, "789 Oak Street", new TimeOnly( 12, 0, 0));
        Store store4 = new Store("GOG Store", game4, "101 Pine Street", new TimeOnly( 13, 0, 0));
 
        Games.AddRange(new[] { game1, game2, game3, game4 });
        Stores.AddRange(new[] { store1, store2, store3, store4 });

        
        
        //store1.Games.add(game1);
        //om de objecten te koppelen via veel op veel relatie
    }
    public void Run()
    {
        bool stoppen = true;
        while (stoppen)
        {
            Console.WriteLine(
                "What would you like to do\n?" +
                "==========================\n" +
                "0) Quit\n" +
                "1) Show all Games\n" +
                "2) Show Games of genre\n" +
                "3) Show all Stores\n" +
                "4) Show stores with Game and/or opening hour\n" +
                "Choice (0-4):\n");
            int  choice = Console.Read();
         

            switch (choice) 
            {
                case 0 : stoppen = false;
                    break;
                case 1 : ShowGames();
                    break;
                case 2 : ShowGamesBasedOnGenre();
                    break;
                case 3: ShowStores();
                    break;
                case 4 : ShowStoresBasedOnGameName();
                    break;
            }
        }
    }

    public void ShowGames()
    {
        foreach (Game game in Games)
        {
            Console.WriteLine(game);
        }
    }
    
    public void ShowGamesBasedOnGenre()
    {
      Console.WriteLine("give genre: 1=actionGenre, 2=adventureGenre");
      int genre = Console.Read();
      foreach (Game game in Games)
      {
          if (Genre.actionGenre.Equals(genre)) 
          {
              Console.WriteLine(game);
          } else if (Genre.adventureGenre.Equals(genre))
          {
              Console.WriteLine(game);
          }
      }
    }
    public void ShowStores()
    {
        foreach (Store store in Stores)
        {
            Console.WriteLine(store);
        }
        
        
    }
    
    public void ShowStoresBasedOnGameName()
    {
        Console.WriteLine("enter the name of a game or leave empty:");
        string game = Console.ReadLine();
        foreach (Store store in Stores)
        {
            if (store.Equals(game))
            { 
                Console.WriteLine(store);
            }
        }
        
        
    }
    
    
    
    
    
}