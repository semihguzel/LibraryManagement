using LibraryManagement.Core.Entities;
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

    public async Task Add(BookCategory book)
    {
        throw new NotImplementedException();
    }

    public async Task Update(BookCategory book)
    {
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