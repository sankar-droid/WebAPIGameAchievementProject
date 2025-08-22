using WebAPIProject.Models;

namespace WebAPIProject.Interface
{
    public interface IToken
    {
        string GenerateToken(User user);
     }
}
