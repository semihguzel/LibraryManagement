using LibraryManagement.Core.Entities;
using LibraryManagement.Core.Helpers;
using LibraryManagement.Core.Helpers.BookHelpers;
using LibraryManagement.Core.Interfaces.Repositories;
using LibraryManagement.Core.Interfaces.Services;

namespace LibraryManagement.Service.Book;

public class BookCategoryService : IBookCategoryService
{
    private readonly IBookCategoryRepository _bookCategoryRepository;

    public BookCategoryService(IBookCategoryRepository bookCategoryRepository)
    {
        _bookCategoryRepository = bookCategoryRepository;
    }

    public async Task Add(BookCategory bookCategory)
    {
        BookCategoryServiceHelper.CheckArgsForException(bookCategory);

        var doesAlreadyExists = await GetByCode(bookCategory.Code) != null;

        if (doesAlreadyExists)
            throw new ArgumentException("This book category already exists.");

        await _bookCategoryRepository.Create(bookCategory);
    }

    public async Task Update(BookCategory book)
    {
        if (book == null)
            throw new ArgumentNullException();
        throw new NotImplementedException();
    }

    public async Task Delete(Guid id)
    {
        throw new NotImplementedException();
    }

    public async Task<BookCategory?> GetByCode(string code)
    {
        if (string.IsNullOrWhiteSpace(code))
            throw new ArgumentException();

        return await _bookCategoryRepository.GetByCode(code);
    }
}