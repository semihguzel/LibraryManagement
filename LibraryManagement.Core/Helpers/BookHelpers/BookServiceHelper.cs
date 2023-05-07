using LibraryManagement.Core.Entities;

namespace LibraryManagement.Core.Helpers.BookHelpers;

public static class BookServiceHelper
{
    public static void CheckArgsForException(Book book)
    {
        if (book == null)
            throw new ArgumentNullException();

        if (string.IsNullOrWhiteSpace(book.Name) || string.IsNullOrWhiteSpace(book.Author) ||
            book.Quantity == 0 ||
            book.PageNumber == 0 || book.BookCategories.Count == 0 || book.CreatedDate == DateTime.MinValue)
            throw new ArgumentException();
    }
}