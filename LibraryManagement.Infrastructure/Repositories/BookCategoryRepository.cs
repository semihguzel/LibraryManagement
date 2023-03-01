using LibraryManagement.Core.Entities;
using LibraryManagement.Core.Interfaces.Repositories;

namespace LibraryManagement.Infrastructure.Repositories;

public class BookCategoryRepository : GenericRepository<BookCategory>, IBookCategoryRepository
{
    private readonly LibraryContext _context;

    public BookCategoryRepository(LibraryContext context) : base(context)
    {
        _context = context;
    }
}