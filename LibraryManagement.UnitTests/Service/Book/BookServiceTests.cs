using LibraryManagement.Core.Interfaces.Repositories;
using LibraryManagement.Core.Interfaces.Services;
using LibraryManagement.Service.Book;
using Moq;
using NUnit.Framework;

namespace LibraryManagement.UnitTests.Service.Book;

[TestFixture]
public class BookServiceTests
{
    private Mock<IBookRepository> _bookRepository;
    private IBookService _bookService;

    [SetUp]
    public void SetUp()
    {
        _bookRepository = new Mock<IBookRepository>();
        _bookService = new BookService(_bookRepository.Object);
    }

    /*
     * TODO:
     * GetById
     * Add
     * Update
     * Delete
     */

    [Test]
    public void GetById_WithEmptyGuid_ThrowArgumentException()
    {
        var id = Guid.Empty;

        Assert.ThrowsAsync<ArgumentException>(() => _bookService.GetById(id));
    }

    [Test]
    public void GetById_NotExistingId_ReturnNull()
    {
        var id = Guid.NewGuid();
        Core.Entities.Book result = null;
        _bookRepository.Setup(x => x.GetByIdAsync(id)).ReturnsAsync(result);

        Assert.That(() => _bookService.GetById(id), Is.Null);
    }

    [Test]
    public void GetById_ExistingId_ReturnObjectWithId()
    {
        var id = Guid.NewGuid();
        Core.Entities.Book result = new Core.Entities.Book { Id = id };
        _bookRepository.Setup(x => x.GetByIdAsync(id)).ReturnsAsync(result);

        Assert.That(() => _bookService.GetById(id), Is.EqualTo(result));
    }
}