using LibraryManagement.Core.Helpers;
using LibraryManagement.Core.Helpers.BookHelpers;
using LibraryManagement.Core.Interfaces.Repositories;
using LibraryManagement.Core.Interfaces.Services;

namespace LibraryManagement.Service.Book;

public class BookService : IBookService
{
    private readonly IBookRepository _bookRepository;
    private readonly IBookCategoryService _bookCategoryService;

    public BookService(IBookRepository bookRepository, IBookCategoryService bookCategoryService)
    {
        _bookRepository = bookRepository;
        _bookCategoryService = bookCategoryService;
    }

    public async Task<Core.Entities.Book?> GetById(Guid id)
    {
        if (id == Guid.Empty)
            throw new ArgumentNullException();

        return await _bookRepository.GetByIdAsync(id);
    }

    public async Task Add(Core.Entities.Book book)
    {
        BookServiceHelper.CheckArgsForException(book);
        
        var doesExists = await _bookRepository.GetByName(book.Name) != null;

        if (doesExists)
            throw new ArgumentException("This book already exists.");

        await _bookRepository.Create(book);
    }

    public async Task Update(Core.Entities.Book book)
    {
        BookServiceHelper.CheckArgsForException(book);
        
        var doesExists = await _bookRepository.GetByName(book.Name) != null;

        if (!doesExists)
            throw new ArgumentException("Book does not exists. Please check the entity.");

        await _bookRepository.Update(book);
    }

    public async Task Delete(Guid id)
    {
        if (id == Guid.Empty)
            throw new ArgumentException();

        var doesExists = await _bookRepository.GetByIdAsync(id) != null;

        if (!doesExists)
            throw new ArgumentException("Book does not exists. Please check the entity.");

        await _bookRepository.Delete(id);
    }

    public async Task<Core.Entities.Book?> GetByName(string name)
    {
        if (string.IsNullOrWhiteSpace(name))
            throw new ArgumentException();

        return await _bookRepository.GetByName(name);
    }

    public async Task<List<Core.Entities.Book>?> GetBooksByCategoryCode(string categoryCode)
    {
        if (string.IsNullOrWhiteSpace(categoryCode))
            throw new ArgumentException();

        var doesCategoryExists = await _bookCategoryService.GetByCode(categoryCode) != null;

        if (!doesCategoryExists)
            throw new ArgumentException("Category does not exists.");

        return await _bookRepository.GetByCategoryCode(categoryCode);
    }

    public async Task<List<Core.Entities.Book>> GetBooksByCategoryId(Guid categoryId)
    {
        throw new NotImplementedException();
    }
}