using LibraryManagement.Core.Interfaces.Repositories;

namespace LibraryManagement.Core.Interfaces;

public interface IUnitOfWork
{
    IBookRepository BookRepository { get; }
    IBookCategoryRepository BookCategoryRepository { get; }
    ILoanRepository LoanRepository { get; }
    IUserRepository UserRepository { get; }
    IUserRoleRepository UserRoleRepository { get; }
    int Save();
    Task<int> SaveAsync();
}