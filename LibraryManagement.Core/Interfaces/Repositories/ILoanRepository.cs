using LibraryManagement.Core.Entities;

namespace LibraryManagement.Core.Interfaces.Repositories;

public interface ILoanRepository : IRepository<Loan>
{
    Task<List<Loan>> GetAllByBookId(Guid bookId);
    Task<List<Loan>> GetAllByUserId(Guid userId);
}