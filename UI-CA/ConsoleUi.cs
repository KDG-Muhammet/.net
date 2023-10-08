using System.Net.Sockets;
using BL;
using Domain;

namespace UI.CA; 

public class ConsoleUi
{

    private IManager _manager;

    public ConsoleUi(IManager manager)
    {
        _manager = manager;
    }
    
    public void Run()
    {
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
        foreach (Game game in _manager.GetAllGames())
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
        
        foreach (Game game in _manager.GetGameOfGenre(nummerOfGenre))
        {
            if (nummerOfGenre == (int)game.Genre)
            {
                Console.WriteLine(game.ToString());
            }
             
        }
    }

    public void ShowStores()
    {
        foreach (var store in _manager.GetAllStore())
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
            intHour = Convert.ToByte(hour);
            
        }
        foreach (var store in _manager.GetStoresByGameNameAndStoreOpeningHour(game, intHour))
        {
         Console.WriteLine( "Store: " + store.Name + " " + "OpeningHour: "  + store.OpeningHour);
         foreach (var currentGame in store.Games)
         {
             Console.WriteLine(" Game: " + currentGame.Name);
         }
        }
    }
    
}