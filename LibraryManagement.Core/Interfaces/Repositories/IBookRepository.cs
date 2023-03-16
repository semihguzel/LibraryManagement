using LibraryManagement.Core.Entities;

namespace LibraryManagement.Core.Interfaces.Repositories;

public interface IBookRepository : IRepository<Book>
{
    Task<Book?> GetByName(string name);
    Task<List<Book>?> GetByCategoryCode(string code);
}