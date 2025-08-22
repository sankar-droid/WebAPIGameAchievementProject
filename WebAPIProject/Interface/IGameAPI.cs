using System.Linq.Expressions;
using WebAPIProject.Models;

namespace WebAPIProject.Interface
{
    public interface IGameAPI<T,R>
    {

        Task<IEnumerable<T>> GetAllAsync();
        Task<T?> GetByIdAsync(R id);
        Task<T> AddAsync(T entity);
        Task<T> UpdateAsync(T entity);
        Task<bool> DeleteAsync(R id);

    }
    public interface IUser
    {
        Task<User> GetByUsernameAsync(string username);
    }
}
