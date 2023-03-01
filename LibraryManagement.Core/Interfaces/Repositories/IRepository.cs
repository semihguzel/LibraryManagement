using LibraryManagement.Core.Entities;

namespace LibraryManagement.Core.Interfaces.Repositories;

public interface IRepository<T> where T : BaseEntity
{
    Task<IReadOnlyList<T>> GetAllAsync();
    Task<T> GetByIdAsync(Guid id);
    Task Create(T entity);
    Task Update(T entity);
    Task Delete(Guid id);
}