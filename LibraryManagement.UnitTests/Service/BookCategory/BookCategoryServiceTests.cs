using LibraryManagement.Core.Interfaces.Repositories;
using LibraryManagement.Infrastructure.Repositories;
using LibraryManagement.Service.Book;
using Moq;
using NUnit.Framework;

namespace LibraryManagement.UnitTests.Service.BookCategory;

[TestFixture]
public class BookCategoryServiceTests
{
    private Mock<IBookCategoryRepository> _bookCategoryRepository;
    private BookCategoryService _bookCategoryService;
    private readonly string _nullString = null;
    private readonly string _whiteSpaceString = "             ";
    private Core.Entities.BookCategory _properBookCategory;

    [SetUp]
    public void SetUp()
    {
        _bookCategoryRepository = new Mock<IBookCategoryRepository>();
        _bookCategoryService = new BookCategoryService(_bookCategoryRepository.Object);
        _properBookCategory = new Core.Entities.BookCategory
        {
            Id = Guid.NewGuid(),
            Code = "scifi",
            Description = "Sci-Fi",
            CreatedDate = DateTime.Now,
            Books = new List<Core.Entities.Book>
            {
                new Core.Entities.Book
                {
                    Id = Guid.NewGuid(), Name = "Silmarillion",
                    BookCategories = new List<Core.Entities.BookCategory>
                        { new Core.Entities.BookCategory { Code = "scifi" } },
                    Author = "Tolkien",
                    Quantity = 1,
                    CreatedDate = DateTime.Now,
                    PageNumber = 873
                }
            }
        };
    }

    [Test]
    public void GetByName_NullOrWhiteSpaceArg_ThrowArgumentException()
    {
        Assert.ThrowsAsync<ArgumentException>(() => _bookCategoryService.GetByCode(_nullString));
        Assert.ThrowsAsync<ArgumentException>(() => _bookCategoryService.GetByCode(_whiteSpaceString));
    }

    [Test]
    public void GetByName_NonExistingCode_ReturnNull()
    {
        Core.Entities.BookCategory nullObject = null;
        var nonExistingCode = "scifi";

        _bookCategoryRepository.Setup(x => x.GetByCode(nonExistingCode)).ReturnsAsync(nullObject);

        Assert.That(() => _bookCategoryService.GetByCode(nonExistingCode), Is.Null);
    }

    [Test]
    public void GetByName_ExistingCode_ReturnRelatedItem()
    {
        var existingCode = "scifi";

        _bookCategoryRepository.Setup(x => x.GetByCode(existingCode)).ReturnsAsync(_properBookCategory);

        Assert.That(() => _bookCategoryService.GetByCode(existingCode), Is.EqualTo(_properBookCategory));
    }
}