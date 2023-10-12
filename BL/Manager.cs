using System.ComponentModel.DataAnnotations;
using DAL;
using Domain;

namespace BL;

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

    public IEnumerable<Game> GetGameOfGenre(int genre)
    {
        return _repository.ReadGameOfGenre(genre);
    }

    public Game AddGame(string name, double? price, Genre genre, DateTime yearReleased, int rating)
    {
        Game newGame = new Game(name, price, genre, yearReleased,  rating) ;
        List<ValidationResult> errors = new List<ValidationResult>();
        Validator.TryValidateObject(newGame, new ValidationContext(newGame), errors, validateAllProperties: true);
        
        
        _repository.CreateGame(newGame);
        return newGame;
    }

    public Store GetStore(int id)
    {
        return _repository.ReadStore(id);
    }

    public IEnumerable<Store> GetAllStore()
    {
        return _repository.ReadAllStore();
    }

    public IEnumerable<Store> GetStoresByGameNameAndStoreOpeningHour(string name, int hour)
    {
        return _repository.ReadStoresByGameNameAndStoreOpeningHour(name, hour);
    }

    public Store AddStore(string name, string address, TimeOnly openingHour)
    {
        Store newStore = new Store(name, address, openingHour);
        List<ValidationResult> errors = new List<ValidationResult>();
        Validator.TryValidateObject(newStore, new ValidationContext(newStore), errors, validateAllProperties: true);

        _repository.CreateStore(newStore);
        return newStore;
    }
}