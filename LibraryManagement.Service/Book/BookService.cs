using LibraryManagement.Core.Helpers;
using LibraryManagement.Core.Interfaces.Repositories;
using LibraryManagement.Core.Interfaces.Services;

namespace LibraryManagement.Service.Book;

public class BookService : IBookService
{
    private readonly IBookRepository _bookRepository;

    public BookService(IBookRepository bookRepository)
    {
        _bookRepository = bookRepository;
    }

    public async Task<Core.Entities.Book?> GetById(Guid id)
    {
        if (id == Guid.Empty)
            throw new ArgumentNullException();

        return await _bookRepository.GetByIdAsync(id);
    }

    public async Task Add(Core.Entities.Book book)
    {
        BookServiceHelper.ThrowExceptionIfArgIsNull(book);

        var doesExists = await _bookRepository.GetByName(book.Name) != null;

        if (doesExists)
            throw new ArgumentException("This book already exists. Please entity and try again");

        BookServiceHelper.CheckArgsForException(book);

        await _bookRepository.Create(book);
    }

    public async Task Update(Core.Entities.Book book)
    {
        if (book == null)
            throw new ArgumentNullException();

        var doesExists = await _bookRepository.GetByName(book.Name) != null;

        if (!doesExists)
            throw new ArgumentException("Book does not exists. Please check the entity.");

        BookServiceHelper.CheckArgsForException(book);

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
}