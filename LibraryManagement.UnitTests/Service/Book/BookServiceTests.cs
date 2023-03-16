using LibraryManagement.Core.Entities;
using LibraryManagement.Core.Helpers;
using LibraryManagement.Core.Interfaces.Repositories;
using LibraryManagement.Core.Interfaces.Services;
using LibraryManagement.Infrastructure.Repositories;
using LibraryManagement.Service.Book;
using Moq;
using NUnit.Framework;

namespace LibraryManagement.UnitTests.Service.Book;

[TestFixture]
public class BookServiceTests
{
    private Mock<IBookRepository> _bookRepository;
    private IBookService _bookService;
    private Core.Entities.Book _bookWithWrongProps;
    private Core.Entities.Book _bookWithProperProps;
    private readonly string _nullString = null;
    private readonly string _whiteSpaceString = "             ";
    private Mock<IBookCategoryRepository> _bookCategoryRepository;
    private IBookCategoryService _bookCategoryService;
    private Core.Entities.BookCategory _properBookCategory;

    #region SetUp

    [SetUp]
    public void SetUp()
    {
        _bookRepository = new Mock<IBookRepository>();
        _bookCategoryRepository = new Mock<IBookCategoryRepository>();
        _bookCategoryService = new BookCategoryService(_bookCategoryRepository.Object);
        _bookService = new BookService(_bookRepository.Object, _bookCategoryService);

        _bookWithWrongProps = new Core.Entities.Book
        {
            Id = Guid.Empty, Name = "",
            BookCategories = new List<Core.Entities.BookCategory>(),
            Author = "",
            Quantity = 0,
            CreatedDate = DateTime.MinValue,
            PageNumber = 0
        };
        _bookWithProperProps = new Core.Entities.Book
        {
            Id = Guid.NewGuid(), Name = "Silmarillion",
            BookCategories =
                new List<Core.Entities.BookCategory> { new() { Code = "scifi" } },
            Author = "Tolkien",
            Quantity = 1,
            CreatedDate = DateTime.Now,
            PageNumber = 873
        };
        _properBookCategory = new Core.Entities.BookCategory
        {
            Id = Guid.NewGuid(),
            Code = "scifi",
            Description = "Sci-Fi",
            CreatedDate = DateTime.Now,
            Books = new List<Core.Entities.Book>
            {
                new()
                {
                    Id = Guid.NewGuid(), Name = "Silmarillion",
                    BookCategories = new List<Core.Entities.BookCategory>
                        { new() { Code = "scifi" } },
                    Author = "Tolkien",
                    Quantity = 1,
                    CreatedDate = DateTime.Now,
                    PageNumber = 873
                }
            }
        };
    }

    #endregion
    
    #region Crud Tests

    [Test]
    public void Add_NullObject_ThrowArgumentNullException()
    {
        Core.Entities.Book book = null;

        Assert.ThrowsAsync<ArgumentNullException>(() => _bookService.Add(book));
    }

    [Test]
    public void Add_ExistingItem_ThrowException()
    {
        string name = "Silmarillion";
        var book = new Core.Entities.Book { Id = Guid.NewGuid(), Name = name };

        _bookRepository.Setup(x => x.GetByName(book.Name)).ReturnsAsync(book);

        var ex = Assert.ThrowsAsync<ArgumentException>(() => _bookService.Add(book));
        Assert.That(ex.Message, Is.EqualTo("This book already exists. Please entity and try again"));
    }

    [Test]
    public void Add_NewItem_CallCreateMethod()
    {
        _bookService.Add(_bookWithProperProps);

        _bookRepository.Verify(x => x.Create(_bookWithProperProps), Times.Once);
    }

    [Test]
    public void Add_WithWrongArgs_ThrowArgumentException()
    {
        Assert.ThrowsAsync<ArgumentException>(() => _bookService.Add(_bookWithWrongProps));
    }

    [Test]
    public void Update_NullObject_ThrowArgumentNullException()
    {
        Core.Entities.Book book = null;

        Assert.ThrowsAsync<ArgumentNullException>(() => _bookService.Update(book));
    }

    [Test]
    public void Update_WithWrongArgs_ThrowArgumentException()
    {
        Assert.ThrowsAsync<ArgumentException>(() => _bookService.Update(_bookWithWrongProps));
    }

    [Test]
    public void Update_NonExistingItem_ThrowArgumentExceptionWithMessage()
    {
        Core.Entities.Book returnObject = null;
        _bookRepository.Setup(x => x.GetByName(_bookWithProperProps.Name)).ReturnsAsync(returnObject);

        var ex = Assert.ThrowsAsync<ArgumentException>(() => _bookService.Update(_bookWithProperProps));

        Assert.That(ex.Message, Is.EqualTo("Book does not exists. Please check the entity."));
    }

    [Test]
    public void Update_ExistingProperArgs_CallUpdateMethod()
    {
        _bookRepository.Setup(x => x.GetByName(_bookWithProperProps.Name)).ReturnsAsync(_bookWithProperProps);

        _bookService.Update(_bookWithProperProps);

        _bookRepository.Verify(x => x.GetByName(_bookWithProperProps.Name), Times.Once);
        _bookRepository.Verify(x => x.Update(_bookWithProperProps), Times.Once);
    }

    /*
     * Delete =>
     *          TODO: book with given id might have a loan, in that case it shouldn't be deleted. - will be written after LoanRepository-LoanService added.
     */

    [Test]
    public void Delete_EmptyGuid_ThrowArgumentException()
    {
        Assert.ThrowsAsync<ArgumentException>(() => _bookService.Delete(_bookWithWrongProps.Id));
    }

    [Test]
    public void Delete_NotExistingId_ThrowArgumentExceptionWithMessage()
    {
        Core.Entities.Book returnObject = null;
        _bookRepository.Setup(x => x.GetByIdAsync(_bookWithProperProps.Id)).ReturnsAsync(returnObject);

        var ex = Assert.ThrowsAsync<ArgumentException>(() => _bookService.Delete(_bookWithProperProps.Id));
        _bookRepository.Verify(x => x.GetByIdAsync(_bookWithProperProps.Id), Times.Once);

        Assert.That(ex.Message, Is.EqualTo("Book does not exists. Please check the entity."));
    }

    [Test]
    public void Delete_ProperArgs_CallDeleteMethod()
    {
        _bookRepository.Setup(x => x.GetByIdAsync(_bookWithProperProps.Id))
            .ReturnsAsync(_bookWithProperProps);

        _bookService.Delete(_bookWithProperProps.Id);

        _bookRepository.Verify(x => x.GetByIdAsync(_bookWithProperProps.Id), Times.Once);
        _bookRepository.Verify(x => x.Delete(_bookWithProperProps.Id), Times.Once);
    }

    #endregion

    #region GetById Tests

    [Test]
    public void GetById_WithEmptyGuid_ThrowArgumentNullException()
    {
        var id = Guid.Empty;

        Assert.ThrowsAsync<ArgumentNullException>(() => _bookService.GetById(id));
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
    public void GetById_ExistingId_ReturnRelatedObject()
    {
        var id = Guid.NewGuid();
        Core.Entities.Book result = new Core.Entities.Book { Id = id };

        _bookRepository.Setup(x => x.GetByIdAsync(id)).ReturnsAsync(result);

        Assert.That(() => _bookService.GetById(id), Is.EqualTo(result));
    }

    #endregion

    #region GetByName Tests

    [Test]
    public void GetByName_NullOrWhiteSpaceArg_ThrowArgumentException()
    {
        Assert.ThrowsAsync<ArgumentException>(() => _bookService.GetByName(_nullString));
        Assert.ThrowsAsync<ArgumentException>(() => _bookService.GetByName(_whiteSpaceString));
    }

    [Test]
    public void GetByName_NonExistingName_ReturnNull()
    {
        string existingName = "Silmarillion";
        Core.Entities.Book nullObject = null;

        _bookRepository.Setup(x => x.GetByName(existingName)).ReturnsAsync(nullObject);

        Assert.That(() => _bookService.GetByName(existingName), Is.Null);
    }

    [Test]
    public void GetByName_ExistingName_ReturnRelatedObject()
    {
        string existingName = "Silmarillion";

        _bookRepository.Setup(x => x.GetByName(existingName)).ReturnsAsync(_bookWithProperProps);

        Assert.That(() => _bookService.GetByName(existingName), Is.EqualTo(_bookWithProperProps));
    }

    #endregion

    #region GetBooksByCategoryName Tests

    [Test]
    public void GetBooksByCategoryCode_NullOrWhiteSpaceArg_ThrowArgumentException()
    {
        Assert.ThrowsAsync<ArgumentException>(() => _bookService.GetBooksByCategoryCode(_nullString));
        Assert.ThrowsAsync<ArgumentException>(() => _bookService.GetBooksByCategoryCode(_whiteSpaceString));
    }

    [Test]
    public void GetBooksByCategoryCode_NonExistingCategory_ThrowArgumentException()
    {
        var nonExistingCategoryCode = "novel";
        Core.Entities.BookCategory nullCategoryObject = null;

        _bookCategoryRepository.Setup(x => x.GetByCode(nonExistingCategoryCode)).ReturnsAsync(nullCategoryObject);

        var ex = Assert.ThrowsAsync<ArgumentException>(() =>
            _bookService.GetBooksByCategoryCode(nonExistingCategoryCode));

        Assert.That(ex.Message, Is.EqualTo("Category does not exists."));
    }

    [Test]
    public void GetBooksByCategoryCode_ExistingCategory_ReturnsEmptyList()
    {
        var existingCategoryName = "Sci-Fi";
        var expectedResultList = new List<Core.Entities.Book>();

        _bookCategoryRepository.Setup(x => x.GetByCode(existingCategoryName)).ReturnsAsync(_properBookCategory);
        _bookRepository.Setup(x => x.GetByCategoryCode(existingCategoryName)).ReturnsAsync(expectedResultList);

        Assert.That(() => _bookService.GetBooksByCategoryCode(existingCategoryName),
            Is.Empty);
    }

    [Test]
    public void GetBooksByCategoryCode_ExistingCategory_ReturnItemsInGivenCategory()
    {
        var existingCategoryName = "Sci-Fi";
        var expectedResultList = new List<Core.Entities.Book> { _bookWithProperProps };

        _bookCategoryRepository.Setup(x => x.GetByCode(existingCategoryName)).ReturnsAsync(_properBookCategory);
        _bookRepository.Setup(x => x.GetByCategoryCode(existingCategoryName)).ReturnsAsync(expectedResultList);


        Assert.That(() => _bookService.GetBooksByCategoryCode(existingCategoryName),
            Contains.Item(_bookWithProperProps));
    }

    #endregion
}