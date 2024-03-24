using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
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
    private readonly UserManager<IdentityUser> _userManager;

    public GamesController(IManager manager,UserManager<IdentityUser> userManager)
    {
        _mgr = manager;
        _userManager = userManager;
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
        Game game = _mgr.GetGame(id);
        if (game == null)
        {
            return NotFound();
        }
        bool isAuthorized = _userManager.GetUserId(User) == game.User?.Id;
        if (!isAuthorized)
        {
            return Unauthorized();
        }
        game.Rating = updateGameDto.Rating;
        _mgr.UpdateRating(game);
            
        return NoContent();
    }
    
}