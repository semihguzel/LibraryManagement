using LibraryManagement.Core.Entities;

namespace LibraryManagement.Core.Helpers.LoanHelpers;

public static class LoanServiceHelper
{
    public static void CheckArgsForException(Loan loan)
    {
        if (loan == null)
            throw new ArgumentNullException();

        if (loan.LentDate == DateTime.MinValue ||
            loan.DueDate == DateTime.MinValue ||
            loan.ReceivedDate == DateTime.MinValue ||
            loan.UserId == Guid.Empty ||
            loan.BookId == Guid.Empty)
            throw new ArgumentException();
    }
}