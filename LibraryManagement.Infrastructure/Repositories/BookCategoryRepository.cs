using LibraryManagement.Core.Entities;
using LibraryManagement.Core.Interfaces.Repositories;
using Microsoft.EntityFrameworkCore;

namespace LibraryManagement.Infrastructure.Repositories;

public class BookCategoryRepository : GenericRepository<BookCategory>, IBookCategoryRepository
{
    private readonly LibraryContext _context;

    public BookCategoryRepository(LibraryContext context) : base(context)
    {
        _context = context;
    }

    public async Task<BookCategory?> GetByCode(string code)
    {
        if (string.IsNullOrWhiteSpace(code))
            throw new ArgumentException();

        return await _context.Set<BookCategory>().FirstOrDefaultAsync(x => x.Code == code);
    }
}