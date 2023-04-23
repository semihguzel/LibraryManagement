using LibraryManagement.Core.Entities;

namespace LibraryManagement.Core.Helpers;

public static class BookCategoryServiceHelper
{
    public static void CheckArgsForException(BookCategory bookCategory)
    {
        if (bookCategory == null)
            throw new ArgumentNullException();

        if (string.IsNullOrEmpty(bookCategory.Code) ||
            bookCategory.CreatedDate == DateTime.MinValue)
            throw new ArgumentException();
    }
}