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

        var doesExist = await _bookRepository.GetByName(book.Name) != null;

        if (doesExist)
            throw new Exception("This book already exists. Please entity and try again");

        BookServiceHelper.CheckArgsForException(book);

        await _bookRepository.Create(book);
    }

    public async Task Update(Core.Entities.Book book)
    {
        throw new NotImplementedException();
    }

    public async Task Delete(Guid id)
    {
        throw new NotImplementedException();
    }
}