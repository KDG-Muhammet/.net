using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using StoreManagement.BL;
using StoreManagement.BL.Domain;
using StoreManagement.UI.Web.MVC.Models.Dto;

namespace StoreManagement.UI.Web.MVC.Controllers.Api;

[ApiController]
[Route("api/[controller]/")]
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
    
    [HttpPut("{id}")]
    public IActionResult UpdateGame(int id ,UpdateGameDto updateGameDto)
    {
        // bool isAuthorized = game.User?.Id == User.Claims.SingleOrDefault( c => c.Type == ClaimTypes.NameIdentifier)?.Value;
        // if (!isAuthorized)
        // {
        //     return Unauthorized();
        // }
        Game game = _mgr.GetGame(id);

        if (game == null)
            return NotFound();

        game.Rating = updateGameDto.Rating;
        //_mgr.UpdateRating(game);
            
        return NoContent();
    }
    
}