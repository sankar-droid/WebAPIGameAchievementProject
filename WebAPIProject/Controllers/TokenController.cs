using CodeFirstApproach.Service;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebAPIProject.DTO;
using WebAPIProject.Models;
using WebAPIProject.Service;

namespace WebAPIProject.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TokenController : ControllerBase
    {
        private readonly GameContext _gameContext;
        private readonly TokenService _genreService;

        public TokenController(TokenService genreService)
        {
            _genreService = genreService;
        }
        [HttpPost]
        public async Task<IActionResult> Post(LoginDTO userData)
        {
            if (userData != null && !string.IsNullOrEmpty(userData.Email) &&
            !string.IsNullOrEmpty(userData.Password))
            {
                var user = await GetUser(userData.Email, userData.Password);
                if (user != null && (user.Role=="Player" || user.Role=="Moderator"))
                {
                    var token = _genreService.GenerateToken(user);

                    return Ok(new { token });

                }
                else
                {
                    return BadRequest("Invalid credentials");
                }
            }
            else
            {
                return BadRequest("Invalid request data");
            }
        }
        private async Task<User> GetUser(string email, string password)
        {
            return await _gameContext.Users.FirstOrDefaultAsync(u => u.Email == email &&
            u.Password == password) ?? new Models.User();
        }

    }
}
