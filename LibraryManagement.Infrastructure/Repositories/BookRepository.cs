using LibraryManagement.Core.Entities;
using LibraryManagement.Core.Interfaces;
using LibraryManagement.Core.Interfaces.Repositories;

namespace LibraryManagement.Infrastructure.Repositories;

public class BookRepository : GenericRepository<Book>, IBookRepository
{
    private readonly LibraryContext _context;

    public BookRepository(LibraryContext context) : base(context)
    {
        _context = context;
    }
}