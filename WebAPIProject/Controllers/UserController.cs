using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration.EnvironmentVariables;
using WebAPIProject.DTO;
using WebAPIProject.Models;
using WebAPIProject.Service;

[Route("api/[controller]")]
[ApiController]
public class UsersController : ControllerBase
{
    private readonly UserService _userService;

    public UsersController(UserService userService)
    {
        _userService = userService;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<User>>> GetUsers()
    {
        var users = await _userService.GetAllAsync();
        if (!users.Any())
            return NotFound("No users found.");

        return Ok(users);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<User>> GetUser(int id)
    {
        var user = await _userService.GetByIdAsync(id);
        if (user == null)
            return NotFound();

        return Ok(user);
    }

    [HttpPost]
    public async Task<ActionResult<User>> PostUser([FromBody] UserDTO user)
    {
        try
        {
            var newUser = new User
            {
                Name = user.Name,
                Email = user.Email,
                Password = user.Password,
                Role = "Player"
            };
            await _userService.AddAsync(newUser);
            return CreatedAtAction(nameof(GetUser), new { id = newUser.UserId }, user);
        }
        catch (ArgumentException ex)
        {
            return BadRequest(ex.Message);
        }
        catch (InvalidOperationException)
        {
            return Conflict("User already exists.");
        }

        
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> PutUser(int id, [FromBody]UserDTO user)
    {

        var entity = await _userService.GetByUsernameAsync(user.Name);
        if(entity == null)
        {
            return NotFound();
        }
        else
        {
            if(entity.UserId == id)
            {
                try
                {
                    await _userService.UpdateAsync(entity);
                    return NoContent();
                }
                catch (KeyNotFoundException)
                {
                    return NotFound();
                }


            }
        }

        return BadRequest("ID mismatch");

    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteUser(int id)
    {
        try
        {
            var entity = _userService.GetByIdAsync(id);
        }
        catch (KeyNotFoundException)
        {
            return NotFound();
        }

        return NoContent();
    }
}

