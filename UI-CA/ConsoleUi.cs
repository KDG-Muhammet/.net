using System.ComponentModel.DataAnnotations;
using StoreManagement.BL;
using StoreManagement.BL.Domain;
using StoreManagement.UI.CA.Extensions;

namespace StoreManagement.UI.CA; 

public class ConsoleUi
{

    private IManager _manager;

    public ConsoleUi(IManager manager)
    {
        _manager = manager;
    }
    
    public void Run()
    {
        bool stop = true;
        while (stop)
        {
            Console.WriteLine(
                "What would you like to do?\n" +
                "==========================\n" +
                "0) Quit\n" +
                "1) Show all Games\n" +
                "2) Show Games of genre\n" +
                "3) Show all Stores\n" +
                "4) Show stores with Game and/or opening hour\n" +
                "5) Add a Game\n" +
                "6) Add a Store\n" +
                "Choice (0-6):\n");
            string choice = Console.ReadLine();


            switch (choice)
            {
                case "0":
                    stop = false;
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
                case "5":
                    NewGame();
                    break;
                case "6":
                    NewStore();
                    break;
            }
        }
    }


    public void ShowGames()
    {
        foreach (Game game in _manager.GetAllGames())
        {
            Console.WriteLine(game.GetInfo());
        }
    }


    public void ShowGamesBasedOnGenre()
    {
        string print = "Give Genre: ";
        foreach (Genre genre in Enum.GetValues(typeof(Genre)))
        {
            print += (int)genre + "=" + genre + " ";
        }
        Console.WriteLine(print);
        int numberOfGenre = int.Parse(Console.ReadLine());
        
        foreach (Game game in _manager.GetGameOfGenre(numberOfGenre))
        {
            Console.WriteLine(game.GetInfo());
        }
    }

    public void ShowStores()
    {
        foreach (var store in _manager.GetAllStore())
        {
            Console.WriteLine(store.GetInfo());
        }
    }
    
    public void ShowStoresBasedOnGameNameAndOpeningHour()
    {
        Console.WriteLine("Enter the name of a game or leave blank:");
        string game = Console.ReadLine();
        Console.WriteLine("Enter a hour or leave blank: ");
        String hour = Console.ReadLine();
        int intHour = 0;
        if (!string.IsNullOrWhiteSpace(hour)) intHour = Convert.ToByte(hour);
        foreach (var store in _manager.GetStoresByGameNameAndStoreOpeningHour(game, intHour))
        {
            Console.WriteLine( "Store: " + store.Name + " " + "OpeningHour: "  + store.OpeningHour);
            foreach (var currentGame in store.Games)
            {
                Console.WriteLine(" Game: " + currentGame.Name);
            }
        }
    }

    public void NewGame()
    {
        bool isValid;
        do
        { 
            Console.WriteLine("Add Game");
            Console.WriteLine("========");
        
            Console.WriteLine("Name: ");
            string game = Console.ReadLine();
        
            Console.WriteLine("price (format: 20,99 default 0,0): ");
            string priceInput = Console.ReadLine();
            double price = string.IsNullOrWhiteSpace(priceInput) ? 0.0 : Convert.ToDouble(priceInput);
        
        
            
            string print = "Genre (default 1): ";
            foreach (Genre genre in Enum.GetValues(typeof(Genre)))
            { 
                print += (int)genre + "=" + genre + " ";
            }
            Console.WriteLine(print);
            string genreInput = Console.ReadLine();
            int intGenre = string.IsNullOrWhiteSpace(genreInput) ? 1 : Convert.ToByte(genreInput);
            Genre selectedGenre = (Genre)intGenre;
        
            Console.WriteLine("yearReleased (format: dd/mm/yyy, default today)");
            string yearReleasedInput = Console.ReadLine();
            DateTime dateTime = string.IsNullOrWhiteSpace(yearReleasedInput) ? DateTime.Today: Convert.ToDateTime(yearReleasedInput);
            DateOnly yearReleased = DateOnly.FromDateTime(dateTime); 
        
            Console.WriteLine("rating (default 0): ");
            string ratingInput = Console.ReadLine();
            int rating = string.IsNullOrWhiteSpace(ratingInput) ? 0 : Convert.ToByte(ratingInput);
        
            try
            { 
                _manager.AddGame(game, price, selectedGenre, yearReleased, rating);
                isValid = false;
            }
            catch (ValidationException e)
            {
                Console.WriteLine("Error:" + e.Message);
                isValid = true;
            }
        
        } while (isValid);

    }
    
    public void NewStore()
    {
        bool isValid;
        do
        {
            Console.WriteLine("Add Store");
            Console.WriteLine("========");
            Console.WriteLine("Name: ");
            string store = Console.ReadLine();
        
            Console.WriteLine("Address: ");
            string address = Console.ReadLine();
        
            Console.WriteLine("openingHour");
            string openingHourInput = Console.ReadLine();
            int openingHour = string.IsNullOrWhiteSpace(openingHourInput) ? 0 : Convert.ToByte(openingHourInput);
        
            try
            { 
                _manager.AddStore(store,address,openingHour);
                isValid = false;
            }
            catch (ValidationException e)
            {
                Console.WriteLine("Error:" + e.Message);
                isValid = true;
            }
        
        } while (isValid);
    }
    
}