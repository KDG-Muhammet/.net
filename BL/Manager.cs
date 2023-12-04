using System.ComponentModel.DataAnnotations;
using System.Text;
using StoreManagement.BL.Domain;
using StoreManagement.DAL;

namespace StoreManagement.BL;

public class Manager : IManager
{

    private readonly IRepository _repository;


    public Manager(IRepository repository)
    {
        _repository = repository;
    }


    public Game GetGame(int id)
    {
        return _repository.ReadGame(id);
    }

    public IEnumerable<Game> GetAllGames()
    {
        return _repository.ReadAllGames();
    }

    public IEnumerable<Game> GetGameOfGenre(Genre genre)
    {
        return _repository.ReadGameOfGenre(genre);
    }

    public Game AddGame(string name, double? price, Genre genre, DateOnly yearReleased, int rating)
    {
       
        Game newGame = new Game(name, price, genre, yearReleased,  rating) ;
        List<ValidationResult> errors = new List<ValidationResult>();
        bool isValid =  Validator.TryValidateObject(newGame, new ValidationContext(newGame), errors, validateAllProperties: true);
        if (!isValid)
        {
            StringBuilder sb = new StringBuilder();
            foreach (ValidationResult validationResult in errors)
            {
                sb.Append(" " + validationResult.ErrorMessage);
            }
            throw new ValidationException(sb.ToString());
        }
       
        _repository.CreateGame(newGame);
        return newGame;
    }

    public IEnumerable<Game> GetAllGamesWithCompany()
    {
        return _repository.ReadAllGamesWithCompany();
    }

    public Store GetStore(int id)
    {
        return _repository.ReadStore(id);
    }

    public IEnumerable<Store> GetAllStore()
    {
        return _repository.ReadAllStore();
    }

    public IEnumerable<Store> GetStoresByStoreNameAndStoreOpeningHour(string name, int hour)
    {
        return _repository.ReadStoresByStoreNameAndStoreOpeningHour(name, hour);
    }

    public Store AddStore(string name, string address, int openingHour)
    {
        Store newStore = new Store(name, address, openingHour);
        List<ValidationResult> errors = new List<ValidationResult>();
       bool isValid = Validator.TryValidateObject(newStore, new ValidationContext(newStore), errors, validateAllProperties: true);

       if (!isValid)
       {
           StringBuilder sb = new StringBuilder();
           foreach (ValidationResult validationResult in errors)
           {
               sb.Append(" " + validationResult.ErrorMessage);
           }
           throw new ValidationException(sb.ToString());
       }
        
        _repository.CreateStore(newStore);
        return newStore;
    }

    public IEnumerable<Store> GetAllStoresWithGames()
    {
        return _repository.ReadAllStoresWithGames();
    }

    public void AddGameToStore(int storeId, int gameId)
    {
        
        GameStore gameStore = new GameStore() { Store = GetStore(storeId), Game = GetGame(gameId), Sales = 100};
        _repository.CreateGameStore(gameStore); 
    }

    public void RemoveGameFromStore(int storeId, int gameId)
    {
        _repository.DeleteGameStore(gameId, storeId);
    }

    public IEnumerable<Game> GetGamesOfStore(int storeId)
    {
       return _repository.ReadGamesOfStore(storeId);
    }
}