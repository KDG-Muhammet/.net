using Microsoft.AspNetCore.Mvc;
using StoreManagement.BL;
using StoreManagement.BL.Domain;
using StoreManagement.UI.Web.MVC.Models.Dto;

namespace StoreManagement.UI.Web.MVC.Controllers.Api;

[ApiController]
[Route("api/[controller]/")]
public class StoresController : ControllerBase
{
   
    private readonly IManager _mgr;

    public StoresController(IManager manager)
    {
        _mgr = manager; 
    }
    
    [HttpGet("{storeId}")]
    public IActionResult GetGames(int storeId)
    {
        IEnumerable<Game> storeGames = _mgr.GetGamesOfStore(storeId);
        
        if (!storeGames.Any())
            return NoContent();

        return Ok(storeGames);
    }
    
    [HttpPost]
    public IActionResult Post(GameStore gameStore)
    {
        _mgr.AddGameToStore(gameStore.Store.Id, gameStore.Game.Id);
        return CreatedAtAction("GetGames", new {id = gameStore.Store.Id}); 
    }
}