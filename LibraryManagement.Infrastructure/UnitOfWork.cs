using LibraryManagement.Core.Interfaces;
using LibraryManagement.Core.Interfaces.Repositories;
using LibraryManagement.Infrastructure.Repositories;

namespace LibraryManagement.Infrastructure;

public class UnitOfWork : IUnitOfWork
{
    private readonly LibraryContext _context;

    public UnitOfWork(LibraryContext context)
    {
        _context = context;
        BookRepository = new BookRepository(_context);
        BookCategoryRepository = new BookCategoryRepository(_context);
        LoanRepository = new LoanRepository(_context);
        UserRepository = new UserRepository(_context);
        UserRoleRepository = new UserRoleRepository(_context);
    }


    public IBookRepository BookRepository { get; }
    public IBookCategoryRepository BookCategoryRepository { get; }
    public ILoanRepository LoanRepository { get; }
    public IUserRepository UserRepository { get; }
    public IUserRoleRepository UserRoleRepository { get; }

    public int Save()
    {
        return _context.SaveChanges();
    }

    public async Task<int> SaveAsync()
    {
        return await _context.SaveChangesAsync();
    }

    public void Dispose()
    {
        _context.Dispose();
    }

    public async Task DisposeAsync()
    {
        await _context.DisposeAsync();
    }
}