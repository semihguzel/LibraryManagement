using LibraryManagement.Core.Entities;
using LibraryManagement.Core.Interfaces;
using LibraryManagement.Core.Interfaces.Repositories;
using Microsoft.EntityFrameworkCore;

namespace LibraryManagement.Infrastructure.Repositories;

public class BookRepository : GenericRepository<Book>, IBookRepository
{
    private readonly LibraryContext _context;

    public BookRepository(LibraryContext context) : base(context)
    {
        _context = context;
    }

    public async Task<Book?> GetByName(string name)
    {
        if (string.IsNullOrWhiteSpace(name))
            throw new ArgumentException();

        return await _context.Set<Book>().FirstOrDefaultAsync(x => x.Name == name);
    }
}