using Microsoft.AspNetCore.Mvc;
using StoreManagement.BL;
using StoreManagement.BL.Domain;
using StoreManagement.UI.Web.MVC.Models;

namespace StoreManagement.UI.Web.MVC.Controllers;

public class GameController : Controller
{
    
    private readonly IManager _mgr;

    public GameController(IManager manager)
    {
        _mgr = manager; 
    }

    public IActionResult Index()
    {
        IEnumerable<Game> games = _mgr.GetAllGames();
        return View(games);
    }
    
    [HttpGet]
    public IActionResult Add()
    {
        return View();
       
    }
    
    [HttpPost]
    public IActionResult Add(NewViewGameModel game)
    {
        if (!ModelState.IsValid)
        {
            return View(game);
        }
        Game newGame = _mgr.AddGame(game.Name, game.Price, game.Genre, game.YearReleased, game.Rating);
        return RedirectToAction("Details", new { id = newGame.Id});
       
    }

    public IActionResult Details(int id)
    {
        Game game = _mgr.GetGameWithStores(id);
        return View(game);
    }
}