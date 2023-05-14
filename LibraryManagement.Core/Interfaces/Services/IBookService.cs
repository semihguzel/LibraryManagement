using LibraryManagement.Core.Entities;

namespace LibraryManagement.Core.Interfaces.Services;

public interface IBookService
{
    Task<Book?> GetById(Guid id);
    Task Add(Book book);
    Task Update(Book book);
    Task Delete(Guid id);
    Task<Book?> GetByName(string name);
    Task<List<Book>?> GetBooksByCategoryCode(string categoryCode);
    Task<IReadOnlyList<Book>> GetAllAsync();
}