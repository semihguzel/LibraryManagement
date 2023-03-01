using LibraryManagement.Core.Entities;
using LibraryManagement.Core.Interfaces;

namespace LibraryManagement.Infrastructure.Repositories;

public class LoanRepository : GenericRepository<Loan>, ILoanRepository
{
    private readonly LibraryContext _context;

    public LoanRepository(LibraryContext context) : base(context)
    {
        _context = context;
    }
}