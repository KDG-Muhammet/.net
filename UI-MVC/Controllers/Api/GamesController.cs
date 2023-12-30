using Microsoft.AspNetCore.Mvc;
using StoreManagement.BL;
using StoreManagement.BL.Domain;

namespace StoreManagement.UI.Web.MVC.Controllers.Api;

[ApiController]
[Route("api/[controller]")]
public class GamesController : ControllerBase
{
    private readonly IManager _mgr;

    public GamesController(IManager manager)
    {
        _mgr = manager; 
    }
    
    [HttpGet]
    public IActionResult GetAllGames()
    {
        IEnumerable<Game> allGames = _mgr.GetAllGames();
        
        if (!allGames.Any())
            return NoContent();
        
        return Ok(allGames);
    }
}