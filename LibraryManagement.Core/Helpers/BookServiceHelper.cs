using LibraryManagement.Core.Entities;

namespace LibraryManagement.Core.Helpers;

public static class BookServiceHelper
{
    public static void ThrowExceptionIfArgIsNull(Book book)
    {
        if (book == null)
            throw new ArgumentNullException();
    }

    public static void CheckArgsForException(Book book)
    {
        if (string.IsNullOrWhiteSpace(book.Name) || string.IsNullOrWhiteSpace(book.Author) || book.Quantity == 0 ||
            book.PageNumber == 0 || book.BookCategories.Count == 0 || book.CreatedDate == DateTime.MinValue)
            throw new ArgumentException();
    }
    
}