using System.Linq.Expressions;
using WebAPIProject.Models;

namespace WebAPIProject.Interface
{
    public interface IGameAPI<T,R>
    {

        Task<IEnumerable<T>> GetAllAsync(params Expression<Func<T, object>>[] includes);
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
