using System.Net.Sockets;
using Domain;

namespace StoreManagement; 

public class ConsoleUi
{
    /*private List<Game> GamesList { get; set; } = new List<Game>();
    private List<Store> StoresList { get; set; } = new List<Store>();

    public void Seed()
    {
        Game game1 = new Game("Assassin's Creed Valhalla", 59.99, Genre.Action, new DateTime(2022, 3, 8, 10, 0, 0), 9);
        Game game2 = new Game("FIFA 21", 49.99, Genre.Action, new DateTime(2021, 9, 10, 10, 0, 0), 8);
        Game game3 = new Game("Cyberpunk 2077", 69.99, Genre.Adventure,new DateTime(2020, 5, 7, 10, 0, 0), 7);
        Game game4 = new Game("The Witcher 3: Wild Hunt", 39.99, Genre.Horror, new DateTime(2023, 7, 13, 10, 0, 0), 9);

        Store store1 = new Store("Steam Store", "123 Main Street", new TimeOnly(10, 0, 0));
        Store store2 = new Store("Epic Games Store", "456 Elm Street", new TimeOnly(11, 0, 0));
        Store store3 = new Store("Ubisoft Store", "789 Oak Street", new TimeOnly(12, 0, 0));
        Store store4 = new Store("GOG Store", "101 Pine Street", new TimeOnly(13, 0, 0));
        
        store1.Games.Add(game1);
        store1.Games.Add(game2);
        store2.Games.Add(game2);
        store3.Games.Add(game3);
        store4.Games.Add(game4);
        
        game1.Stores.Add(store1);
        game1.Stores.Add(store2);
        game3.Stores.Add(store1);
        game4.Stores.Add(store2);
        
        

        GamesList.AddRange(new[] { game1, game2, game3, game4 });
        StoresList.AddRange(new[] { store1, store2, store3, store4 });
    }*/


    public void Run()
    {
        Seed();
        bool stoppen = true;
        while (stoppen)
        {
            Console.WriteLine(
                "What would you like to do?\n" +
                "==========================\n" +
                "0) Quit\n" +
                "1) Show all Games\n" +
                "2) Show Games of genre\n" +
                "3) Show all Stores\n" +
                "4) Show stores with Game and/or opening hour\n" +
                "Choice (0-4):\n");
            string choice = Console.ReadLine();


            switch (choice)
            {
                case "0":
                    stoppen = false;
                    break;
                case "1":
                    ShowGames();
                    break;
                case "2":
                    ShowGamesBasedOnGenre();
                    break;
                case "3":
                    ShowStores();
                    break;
                case "4":
                    ShowStoresBasedOnGameNameAndOpeningHour();
                    break;
            }
        }
    }


    public void ShowGames()
    {
        foreach (Game game in GamesList)
        {
            Console.WriteLine(game.ToString());
        }
    }


    public void ShowGamesBasedOnGenre()
    {
        string print = "Give Genre: ";
        foreach (Genre genre in Enum.GetValues(typeof(Genre)))
        {
            print += (int)genre + "=" + genre.ToString() + " ";
        }
        Console.WriteLine(print);
        int nummerOfGenre = int.Parse(Console.ReadLine());
        foreach (Game game in GamesList)
        {
            if (nummerOfGenre == (int)game.Genre)
            {
                Console.WriteLine(game.ToString());
            }
            
        }
    }

    public void ShowStores()
    {
        foreach (Store store in StoresList)
        {
            Console.WriteLine(store.ToString());
        }
    }


    public void ShowStoresBasedOnGameNameAndOpeningHour()
    {
        Console.WriteLine("Enter the name of a game or leave blank:");
        string game = Console.ReadLine();
        Console.WriteLine("Enter a hour or leave blank: ");
        String hour = Console.ReadLine();
        int intHour = 0;
        if (!string.IsNullOrWhiteSpace(hour))
        {
            intHour = Int16.Parse(hour);
            
        }
        foreach (Store store in StoresList )
        {
            if (intHour == 0 || store.OpeningHour.Hour.Equals(intHour)) 
            {
                foreach (Game storeGame in store.Games)
                {
                    if ( string.IsNullOrWhiteSpace(game) || storeGame.Name.Equals(game))
                    {
                        Console.WriteLine( "Store: " + store.Name + " " + "OpeningHour: "  + store.OpeningHour + " Game: " + storeGame.Name);
                    } 
                }
                
            }
            
        }
    }
    
}