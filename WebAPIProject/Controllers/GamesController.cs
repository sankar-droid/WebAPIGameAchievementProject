using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using System.Linq.Expressions;
using WebAPIProject.DTO;
using WebAPIProject.Models;
using WebAPIProject.Service;

[Route("api/[controller]")]
[ApiController]
public class GamesController : ControllerBase
{
    private readonly GameService<Game> _gameService;

    public GamesController(GameService<Game> gameService)
    {
        _gameService = gameService;
    }

    // GET: api/Games
    [HttpGet]
    [Authorize(Roles = "Moderator,Player")]
    public async Task<ActionResult<IEnumerable<Game>>> GetGames()
    {
        var games = await _gameService.GetAllAsync();

        if (!games.Any())
            return NotFound("No games available.");

        return Ok(games);
    }

    // GET: api/Games/5
    [HttpGet("{id}")]
    [Authorize(Roles = "Moderator,Player")]
    public async Task<ActionResult<Game>> GetGame(string id)
    {
        var game = await _gameService.GetByIdAsync(id);

        if (game == null)
            return NotFound();

        return Ok(game);
    }

    // PUT: api/Games/5
    [HttpPut("{id}")]
    [Authorize(Roles = "Moderator")]
    public async Task<IActionResult> PutGame(string id, Game game)
    {
        if (id != game.GameId)
            return BadRequest("ID mismatch");

        try
        {
            await _gameService.UpdateAsync(game);
        }
        catch (KeyNotFoundException)
        {
            return NotFound();
        }

        return NoContent();
    }

    // POST: api/Games
    [HttpPost]
    [Authorize(Roles = "Moderator,Player")]
    public async Task<ActionResult<Game>> PostGame([FromBody]GameDTO game)
    {
        try
        {
           IEnumerable<Game> Games = await _gameService.GetAllAsync();

            var latestgame=Games.LastOrDefault();
            var newgame = new Game
            {

                GameId = "GAME" + (int.Parse(latestgame.GameId.Substring(4)) + 1),
                GameName = game.GameName,
                Description = game.Description,
                ReleaseYear = game.ReleaseYear,


            }; 
            await _gameService.AddAsync(newgame);
            return CreatedAtAction(nameof(GetGame), new { id = newgame.GameId }, newgame);
        }
        catch (ArgumentException ex)
        {
            return BadRequest(ex.Message);
        }
        catch (InvalidOperationException)
        {
            return Conflict("Game already exists.");
        }
        
    }

    // DELETE: api/Games/5
    [HttpDelete("{id}")]
    [Authorize(Roles = "Moderator")]
    public async Task<IActionResult> DeleteGame(string id)
    {
        try
        {
            await _gameService.DeleteAsync(id);
        }
        catch (KeyNotFoundException)
        {
            return NotFound();
        }

        return NoContent();
    }
}
