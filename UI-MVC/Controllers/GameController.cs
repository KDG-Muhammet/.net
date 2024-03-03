using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using StoreManagement.BL;
using StoreManagement.BL.Domain;
using StoreManagement.UI.Web.MVC.Models;

namespace StoreManagement.UI.Web.MVC.Controllers;

public class GameController : Controller
{
    
    private readonly IManager _mgr;
    private readonly UserManager<IdentityUser> _userManager;


    public GameController(IManager manager, UserManager<IdentityUser> userManager)
    {
        _mgr = manager; 
        _userManager = userManager;
    }

    public IActionResult Index()
    {
        IEnumerable<Game> games = _mgr.GetAllGames();
        return View(games);
    }
    
    [Authorize]
    [HttpGet]
    public IActionResult Add()
    {
        return View();
       
    }
    
    [Authorize]
    [HttpPost]
    public IActionResult Add(NewViewGameModel game)
    {
        if (!ModelState.IsValid)
        {
            return View(game);
        }
        Game newGame = _mgr.AddGame(game.Name, game.Price, game.Genre, game.YearReleased, game.Rating);
        var currentUser = _userManager.GetUserAsync(User).Result;
        newGame.User = currentUser;
        return RedirectToAction("Details", new { id = newGame.Id});
       
    }

    public IActionResult Details(int id)
    {
        Game game = _mgr.GetGameWithStores(id);
        return View(game);
    }
}