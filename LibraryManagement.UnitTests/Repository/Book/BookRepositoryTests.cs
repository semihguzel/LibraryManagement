using LibraryManagement.Core.Interfaces.Repositories;
using LibraryManagement.Infrastructure.Repositories;
using Moq;
using NUnit.Framework;

namespace LibraryManagement.UnitTests.Repository.Book;

[TestFixture]
public class BookRepositoryTests
{
    private IBookRepository _bookRepository;
    private Mock<IBookRepository> _bookRepositoryMock;

    [SetUp]
    public void SetUp()
    {
        _bookRepository = new BookRepository(null);
        _bookRepositoryMock = new Mock<IBookRepository>();
    }

    [TestCase("")]
    [TestCase(null)]
    [TestCase("              ")]
    public void GetByName_NullOrWhiteSpace_ArgumentException(string name)
    {
        Assert.ThrowsAsync<ArgumentException>(() => _bookRepository.GetByName(name));
    }

    [Test]
    public async Task GetByName_NotExisting_ReturnNull()
    {
        string name = "Silmarillion";
        Core.Entities.Book result = null;

        _bookRepositoryMock.Setup(x => x.GetByName(name)).ReturnsAsync(result);

        Assert.That(() => _bookRepositoryMock.Object.GetByName(name), Is.EqualTo(result));
    }
    
    [Test]
    public async Task GetByName_ExistingName_ReturnItem()
    {
        string name = "Silmarillion";
        Core.Entities.Book result = new Core.Entities.Book {Name = "Silmarillion"};

        _bookRepositoryMock.Setup(x => x.GetByName(name)).ReturnsAsync(result);

        Assert.That(() => _bookRepositoryMock.Object.GetByName(name), Is.EqualTo(result));
    }
}