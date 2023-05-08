using LibraryManagement.Core.Interfaces.Repositories;
using LibraryManagement.Core.Interfaces.Services;
using LibraryManagement.Service.Loan;
using Moq;
using NUnit.Framework;

namespace LibraryManagement.UnitTests.Service.Loan;

[TestFixture]
public class LoanServiceTests
{
    private Mock<ILoanRepository> _loanRepository;
    private ILoanService _loanService;

    private Core.Entities.Loan _properObj;
    private Core.Entities.Loan _improperObj;

    [SetUp]
    public void SetUp()
    {
        _loanRepository = new Mock<ILoanRepository>();
        _loanService = new LoanService(_loanRepository.Object);

        _properObj = new Core.Entities.Loan
        {
            CreatedDate = DateTime.Now,
            DueDate = DateTime.Today.AddDays(5),
            ReceivedDate = DateTime.Today.AddDays(1),
            LentDate = DateTime.Today,
            BookId = Guid.NewGuid(),
            UserId = Guid.NewGuid()
        };
        _improperObj = new Core.Entities.Loan
        {
            Id = Guid.Empty,
            CreatedDate = DateTime.MinValue,
            DueDate = DateTime.MinValue,
            ReceivedDate = DateTime.MinValue,
            LentDate = DateTime.MinValue,
            BookId = Guid.Empty,
            UserId = Guid.Empty
        };
    }

    #region Crud

    #region Add

    [Test]
    public void Add_NullObject_ThrowArgumentNullException()
    {
        Core.Entities.Loan nullObj = null;
        Assert.ThrowsAsync<ArgumentNullException>(() => _loanService.Add(nullObj));
    }

    [Test]
    public void Add_EmptyEntity_ThrowArgumentException()
    {
        Assert.ThrowsAsync<ArgumentException>(() => _loanService.Add(_improperObj));
    }

    [Test]
    public void Add_ExistingItem_ThrowExceptionWithMessage()
    {
        _loanRepository.Setup(x => x.GetAllByUserId(_properObj.UserId))
            .ReturnsAsync(new List<Core.Entities.Loan>() { _properObj });

        var ex = Assert.ThrowsAsync<Exception>(() => _loanService.Add(_properObj));
        Assert.That(ex.Message, Is.EqualTo("This book is already lent to the person."));
    }

    [Test]
    public void Add_WithProperArgs_CallCreateMethod()
    {
        _loanService.Add(_properObj);
        _loanRepository.Verify(x => x.Create(_properObj), Times.Once);
    }

    #endregion

    #region Update

    [Test]
    public void Update_NullObj_ThrowArgumentNullException()
    {
        Core.Entities.Loan nullObj = null;
        Assert.ThrowsAsync<ArgumentNullException>(() => _loanService.Update(nullObj));
    }

    [Test]
    public void Update_WithEmptyEntity_ThrowArgumentException()
    {
        Assert.ThrowsAsync<ArgumentException>(() => _loanService.Update(_improperObj));
    }

    [Test]
    public void Update_WithNonExistingObj_ThrowExceptionWithMessage()
    {
        Core.Entities.Loan nullObj = null;

        _loanRepository.Setup(x => x.GetByIdAsync(_properObj.Id)).ReturnsAsync(nullObj);

        var ex = Assert.ThrowsAsync<Exception>(() => _loanService.Update(_properObj));
        Assert.That(ex.Message, Is.EqualTo("Lent item couldn't have been found. Please check sent arguments."));
    }

    [Test]
    public void Update_WithProperArgs_CallUpdateMethod()
    {
        _loanRepository.Setup(x => x.GetByIdAsync(_properObj.Id)).ReturnsAsync(_properObj);

        _loanService.Update(_properObj);

        _loanRepository.Verify(x => x.GetByIdAsync(_properObj.Id), Times.Once);
        _loanRepository.Verify(x => x.Update(_properObj));
    }

    #endregion

    #region Delete

    [Test]
    public void Delete_WithEmptyGuid_ThrowArgumentExceptionWithMessage()
    {
        var ex = Assert.ThrowsAsync<ArgumentException>(() => _loanService.Delete(_improperObj.Id));

        Assert.That(ex.Message, Is.EqualTo("Please enter a valid id."));
    }

    [Test]
    public void Delete_WithNonExistingId_ThrowArgumentExceptionWithMessage()
    {
        Core.Entities.Loan nullObj = null;
        _loanRepository.Setup(x => x.GetByIdAsync(_properObj.Id)).ReturnsAsync(nullObj);

        var ex = Assert.ThrowsAsync<ArgumentException>(() => _loanService.Delete(_properObj.Id));
        Assert.That(ex.Message, Is.EqualTo("Lent item couldn't have been found. Please check sent arguments."));
    }

    #endregion

    #endregion
}