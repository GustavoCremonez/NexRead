namespace NexRead.Domain.Interfaces;

public interface IGeneralRepository<T> where T : class
{
    Task AddAsync(T entity);

    void Update(T entity);

    void Delete(T entity);

    Task<T> GetByIdAsync(Guid id, bool asNoTracking = false);

    Task<IEnumerable<T>> GetAllAsync(bool asNoTracking = false);

    Task SaveChangesAsync();
}
