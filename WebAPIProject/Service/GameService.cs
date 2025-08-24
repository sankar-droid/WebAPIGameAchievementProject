using System.Linq.Expressions;
using WebAPIProject.Interface;

public class GameService<T, R> where T : class
{
    private readonly IGameAPI<T, R> _repository;

    public GameService(IGameAPI<T, R> repository)
    {
        _repository = repository;
    }

    public async Task<IEnumerable<T>> GetAllAsync(params Expression<Func<T, object>>[] includes)
    {
        return await _repository.GetAllAsync(includes);
    }


    public async Task<T?> GetByIdAsync(R id)
    {
        return await _repository.GetByIdAsync(id);
    }

    public async Task<T> AddAsync(T entity)
    {
        return await _repository.AddAsync(entity);
    }

    public async Task<T> UpdateAsync(T entity)
    {
        return await _repository.UpdateAsync(entity);
    }

    public async Task<bool> DeleteAsync(R id)
    {
        return await _repository.DeleteAsync(id);
    }
}
