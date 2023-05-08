using LibraryManagement.Core.Helpers.LoanHelpers;
using LibraryManagement.Core.Interfaces.Repositories;
using LibraryManagement.Core.Interfaces.Services;

namespace LibraryManagement.Service.Loan;

public class LoanService : ILoanService
{
    private ILoanRepository _loanRepository;

    public LoanService(ILoanRepository loanRepository)
    {
        _loanRepository = loanRepository;
    }


    public async Task Add(Core.Entities.Loan loan)
    {
        if (loan == null)
            throw new ArgumentNullException();

        LoanServiceHelper.CheckArgsForException(loan);

        var userLentList = await _loanRepository.GetAllByUserId(loan.UserId);

        if (userLentList != null && userLentList.Count > 0)
        {
            var doesAlreadyLentBook = userLentList.Any(x => x.BookId == loan.BookId);

            if (doesAlreadyLentBook)
                throw new Exception("This book is already lent to the person.");
        }

        await _loanRepository.Create(loan);
    }

    public async Task Update(Core.Entities.Loan loan)
    {
        if (loan == null)
            throw new ArgumentNullException();

        LoanServiceHelper.CheckArgsForException(loan);

        var doesExists = await _loanRepository.GetByIdAsync(loan.Id) != null;

        if (!doesExists)
            throw new Exception("Lent item couldn't have been found. Please check sent arguments.");

        await _loanRepository.Update(loan);
    }

    public async Task Delete(Guid id)
    {
        if (id == Guid.Empty)
            throw new ArgumentException("Please enter a valid id.");

        var doesExists = await _loanRepository.GetByIdAsync(id) != null;

        if (!doesExists)
            throw new ArgumentException("Lent item couldn't have been found. Please check sent arguments.");

        await _loanRepository.Delete(id);
    }

    public async Task<Core.Entities.Loan?> GetById(Guid id)
    {
        if (id == Guid.Empty)
            throw new ArgumentException("Please enter a valid id.");

        return await _loanRepository.GetByIdAsync(id);
    }

    public async Task<List<Core.Entities.Loan>?> GetAllByBookId(Guid bookId)
    {
        if (bookId == Guid.Empty)
            throw new ArgumentException("Please enter a valid id.");

        return await _loanRepository.GetAllByBookId(bookId);
    }

    public async Task<List<Core.Entities.Loan>> GetAllByUserId(Guid userId)
    {
        if (userId == Guid.Empty)
            throw new ArgumentException("Please enter a valid id.");

        return await _loanRepository.GetAllByUserId(userId);
    }
}