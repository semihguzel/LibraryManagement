using LibraryManagement.Core.Entities;

namespace LibraryManagement.Core.Interfaces.Repositories;

public interface IBookCategoryRepository : IRepository<BookCategory>
{
    Task<BookCategory?> GetByCode(string code);
}