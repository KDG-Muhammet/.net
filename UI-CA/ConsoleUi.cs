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
                "7) Add game to store\n" +
                "8) Remove game from store\n" +
                "Choice (0-8):\n");
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
                case "7":
                    AddGameToStore();
                    break;
                case "8":
                    RemoveGameFromStore();
                    break;
                
            }
        }
    }


    public void ShowGames()
    {
        // foreach (Game game in _manager.GetAllGames())
        // {
        //     Console.WriteLine(game.GetInfo());
        // }
        
        foreach (Game game in _manager.GetAllGamesWithCompany())
        {
            Console.WriteLine("{0} [created by {1}]", game.GetInfo(), game.Company == null ? "?" : game.Company.Name);  
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
        
        foreach (Game game in _manager.GetGameOfGenre((Genre)numberOfGenre))
        {
            Console.WriteLine(game.GetInfo());
        }
    }

    public void ShowStores()
    {
        // foreach (var store in _manager.GetAllStore())
        // {
        //     Console.WriteLine(store.GetInfo());
        // }
        
        foreach (Store store in _manager.GetAllStoresWithGames())
        {
            Console.WriteLine("{0}", store.GetInfo());
            foreach (GameStore game in store.Game)
            {
                Console.WriteLine("     Game: {0}", game.Game.Name);
            }
        }
    }
    
    public void ShowStoresBasedOnGameNameAndOpeningHour()
    {
        Console.WriteLine("Enter the name of a store or leave blank:");
        string game = Console.ReadLine();
        Console.WriteLine("Enter a hour or leave blank: ");
        String hour = Console.ReadLine();
        int intHour = 0;
        if (!string.IsNullOrWhiteSpace(hour)) intHour = Convert.ToByte(hour);
        foreach (var store in _manager.GetStoresByStoreNameAndStoreOpeningHour(game, intHour))
        {
            Console.WriteLine( "Store: " + store.Name + " " + "OpeningHour: "  + store.OpeningHour);
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

    public void AddGameToStore()
    {
        Console.WriteLine("Which store would you like to add a game to?");
        foreach (Store store in _manager.GetAllStore())
        {
            Console.WriteLine("[{0}] {1}", store.Id, store.Name);
        }
        Console.WriteLine("Please enter an store ID: ");
        int storeId = int.Parse(Console.ReadLine() ?? string.Empty);
        
        Console.WriteLine("Which game would you like to assign to this store?");
        foreach (Game game in _manager.GetAllGames())
        {
            Console.WriteLine("[{0}] {1}", game.Id, game.Name);
        }
        Console.WriteLine("Please enter an game ID: ");
        int gameId = int.Parse(Console.ReadLine() ?? string.Empty);

        _manager.AddGameToStore(storeId,gameId);
    }

    public void RemoveGameFromStore()
    {
        Console.WriteLine("Which store would you like to remove a game from?");
        foreach (Store store in _manager.GetAllStore())
        {
            Console.WriteLine("[{0}] {1}", store.Id, store.Name);
        }
        Console.WriteLine("Please enter an store ID: ");
        int storeId = int.Parse(Console.ReadLine() ?? string.Empty);
        
        Console.WriteLine("Which game would you like to remove from this store?");
        foreach (Game game in _manager.GetGamesOfStore(storeId))
        {
            Console.WriteLine("[{0}] {1}", game.Id, game.Name);
        }
        Console.WriteLine("Please enter an game ID: ");
        int gameId = int.Parse(Console.ReadLine() ?? string.Empty);

        _manager.RemoveGameFromStore(storeId,gameId);
    }
    
    
}