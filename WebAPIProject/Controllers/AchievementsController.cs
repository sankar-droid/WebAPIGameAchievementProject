using Humanizer.Localisation;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebAPIProject.DTO;
using WebAPIProject.Models;
using WebAPIProject.Service;

[Route("api/[controller]")]
[ApiController]
public class AchievementsController : ControllerBase
{
    private readonly GameService<Achievements,string> _achievementService;

    public AchievementsController(GameService<Achievements, string> achievementService)
    {
        _achievementService = achievementService;
    }

    [HttpGet]
    [Authorize(Roles ="Moderator,Player")]
    public async Task<ActionResult<IEnumerable<Achievements>>> GetAchievements()
    {
        var achievements = await _achievementService.GetAllAsync(a => a.Game);
        if (!achievements.Any())
            return NotFound("No achievements found.");

        return Ok(achievements);
    }

    [HttpGet("{id}")]
    [Authorize(Roles ="Moderator,Player")]
    public async Task<ActionResult<Achievements>> GetAchievement(string id)
    {
        var achievement = await _achievementService.GetByIdAsync(id);
        if (achievement == null)
            return NotFound();

        return Ok(achievement);
    }

    [HttpPost]
    [Authorize(Roles =("Moderator"))]
    public async Task<ActionResult<Achievements>> PostAchievement([FromBody] AchievementsDTO achievement)
    {
        try
        {
            IEnumerable<Achievements> achievements = await _achievementService.GetAllAsync();

            var latestachievement = achievements.LastOrDefault();
            var newachievement = new Achievements
            {
                AchievementId = latestachievement != null
                    ? "ACH" + (int.Parse(latestachievement.AchievementId.Substring(3)) + 1)
                    : "ACH1",
                AchievementName = achievement.name,
                Badge = achievement.Badge,
                Difficulty = achievement.Difficulty,
                Reward = achievement.Reward, 
                GameId = achievement.GameId
            };
            await _achievementService.AddAsync(newachievement);
            return CreatedAtAction(nameof(GetAchievement), new { id = newachievement.AchievementId }, newachievement);
        }
        catch (ArgumentException ex)
        {
            return BadRequest(ex.Message);
        }
        catch (InvalidOperationException)
        {
            return Conflict("Achievement already exists.");
        }
    }

    [HttpPut("{id}")]
    [Authorize(Roles ="Moderator")]
    public async Task<IActionResult> PutAchievement(string id, Achievements achievement)
    {
        if (id != achievement.AchievementId)
            return BadRequest("ID mismatch");

        try
        {
            await _achievementService.UpdateAsync(achievement);
        }
        catch (KeyNotFoundException)
        {
            return NotFound();
        }

        return NoContent();
    }

    [HttpDelete("{id}")]
    [Authorize(Roles ="Moderator")]
    public async Task<IActionResult> DeleteAchievement(string id)
    {
        try
        {
            await _achievementService.DeleteAsync(id);
        }
        catch (KeyNotFoundException)
        {
            return NotFound();
        }

        return NoContent();
    }
    [HttpGet("difficulty/{level}")]
    public async Task<ActionResult<IEnumerable<Achievements>>> GetByDifficulty(string level)
    {
        var achievements = await _achievementService.GetAllAsync();
        var result = achievements.Where(a => a.Difficulty.Equals(level, StringComparison.OrdinalIgnoreCase));
        return Ok(result);
    }
}
