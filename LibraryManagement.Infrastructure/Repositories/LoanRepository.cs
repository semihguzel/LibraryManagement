using LibraryManagement.Core.Entities;
using LibraryManagement.Core.Interfaces;
using LibraryManagement.Core.Interfaces.Repositories;
using Microsoft.EntityFrameworkCore;

namespace LibraryManagement.Infrastructure.Repositories;

public class LoanRepository : GenericRepository<Loan>, ILoanRepository
{
    private readonly LibraryContext _context;

    public LoanRepository(LibraryContext context) : base(context)
    {
        _context = context;
    }

    public async Task<List<Loan>> GetAllByBookId(Guid bookId)
    {
        return await _context.Loans.Where(x => x.BookId == bookId).ToListAsync();
    }

    public async Task<List<Loan>> GetAllByUserId(Guid userId)
    {
        return await _context.Loans.Where(x => x.UserId == userId).ToListAsync();
    }
}