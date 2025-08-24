using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebAPIProject.DTO;
using WebAPIProject.Models;
using WebAPIProject.Service;

[Route("api/[controller]")]
[ApiController]
public class GameGenreController : ControllerBase
{
    private readonly GameService<GameGenre, string> _genreService;

    public GameGenreController(GameService<GameGenre, string> genreService)
    {
        _genreService = genreService;
    }

    [HttpGet]
    [Authorize(Roles = "Moderator,Player")]
    public async Task<ActionResult<IEnumerable<GameGenre>>> GetGenres()
    {
        var genres = await _genreService.GetAllAsync(g => g.Games);
        if (!genres.Any())
            return NotFound("No genres found.");

        return Ok(genres);
    }

    [HttpGet("{id}")]
    [Authorize(Roles = "Moderator,Player")]
    public async Task<ActionResult<GameGenre>> GetGenre(string id)
    {
        var genre = await _genreService.GetByIdAsync(id);
        if (genre == null)
            return NotFound();

        return Ok(genre);
    }

    [HttpPost]
    [Authorize(Roles = "Moderator")]
    public async Task<ActionResult<GameGenre>> PostGenre([FromBody] GameGenreDTO genre)
    {
        try
        {
            IEnumerable<GameGenre> Games = await _genreService.GetAllAsync();

            var latestgenre = Games.LastOrDefault();
            var newgenre = new GameGenre
            {

                GameGenreId = "GEN" + (int.Parse(latestgenre.GameGenreId.Substring(3)) + 1),
                 GenreName= genre.GenreName,
                Description = genre.Description,
                Popularity = latestgenre.Popularity


            };
            await _genreService.AddAsync(newgenre);
            return CreatedAtAction(nameof(GetGenre), new { id = newgenre.GameGenreId }, newgenre);
        }
        catch (ArgumentException ex)
        {
            return BadRequest(ex.Message);
        }
        catch (InvalidOperationException)
        {
            return Conflict("Genre already exists.");
        }
    }

    [HttpPut("{id}")]
    [Authorize(Roles = "Moderator")]
    public async Task<IActionResult> PutGenre(string id, GameGenre genre)
    {
        if (id != genre.GameGenreId)
            return BadRequest("ID mismatch");

        try
        {
            await _genreService.UpdateAsync(genre);
        }
        catch (KeyNotFoundException)
        {
            return NotFound();
        }

        return NoContent();
    }

    [HttpDelete("{id}")]
    [Authorize(Roles = "Moderator")]
    public async Task<IActionResult> DeleteGenre(string id)
    {
        try
        {
            await _genreService.DeleteAsync(id);
        }
        catch (KeyNotFoundException)
        {
            return NotFound();
        }

        return NoContent();
    }
}
