using LibraryManagement.Core.Entities;
using LibraryManagement.Core.Interfaces.Repositories;
using LibraryManagement.Core.Interfaces.Services;
using LibraryManagement.Service.User;
using Moq;
using NUnit.Framework;

namespace LibraryManagement.UnitTests.Service.User;

public class UserServiceTests
{
    private Mock<IUserRepository> _userRepository;
    private IUserService _userService;

    private Core.Entities.User _properUser;
    private Core.Entities.User _improperUser;

    #region SetUp

    [SetUp]
    public void SetUp()
    {
        _userRepository = new Mock<IUserRepository>();
        _userService = new UserService(_userRepository.Object);

        _properUser = new Core.Entities.User
        {
            Username = "semih123",
            Email = "email",
            Id = Guid.NewGuid(),
            Phone = "123432",
            CreatedDate = DateTime.Now,
            UserRoles = new List<UserRole> { new() { Name = "User", CreatedDate = DateTime.Now } }
        };

        _improperUser = new Core.Entities.User
        {
            Username = "",
            Email = "",
            Id = Guid.Empty,
            CreatedDate = DateTime.MinValue,
            Phone = "",
        };
    }

    #endregion

    #region Crud

    #region Add

    [Test]
    public void Add_NullObject_ThrowArgumentNullException()
    {
        Core.Entities.User nullUser = null;
        Assert.ThrowsAsync<ArgumentNullException>(() => _userService.Add(nullUser));
    }

    [Test]
    public void Add_EmptyEntity_ThrowArgumentException()
    {
        Core.Entities.User nullUser = new Core.Entities.User();
        Assert.ThrowsAsync<ArgumentException>(() => _userService.Add(nullUser));
    }

    [Test]
    public void Add_ExistingItem_ThrowExceptionWithMessage()
    {
        _userRepository.Setup(x => x.GetByUsername(_properUser.Username)).ReturnsAsync(_properUser);

        var ex = Assert.ThrowsAsync<ArgumentException>(() => _userService.Add(_properUser));

        Assert.That(ex.Message, Is.EqualTo("This username already exists."));
    }

    [Test]
    public void Add_WithProperArgs_CallCreateMethod()
    {
        _userService.Add(_properUser);
        _userRepository.Verify(x => x.Create(_properUser), Times.Once);
    }

    #endregion

    #region Update

    [Test]
    public void Update_WithNullObject_ThrowArgumentNullException()
    {
        Core.Entities.User nullUser = null;

        Assert.ThrowsAsync<ArgumentNullException>(() => _userService.Update(nullUser));
    }

    [Test]
    public void Update_WithWrongArgs_ThrowArgumentException()
    {
        Assert.ThrowsAsync<ArgumentException>(() => _userService.Update(_improperUser));
    }

    [Test]
    public void Update_WithNonExistingItem_ThrowArgumentExceptionWithMessage()
    {
        Core.Entities.User returnObject = null;

        _userRepository.Setup(x => x.GetByUsername(_properUser.Username)).ReturnsAsync(returnObject);

        var ex = Assert.ThrowsAsync<ArgumentException>(() => _userService.Update(_properUser));

        Assert.That(ex.Message, Is.EqualTo("User does not exists. Please check the entity."));
    }

    [Test]
    public void Update_WithProperArgs_CallUpdateMethod()
    {
        _userRepository.Setup(x => x.GetByUsername(_properUser.Username)).ReturnsAsync(_properUser);

        _userService.Update(_properUser);

        _userRepository.Verify(x => x.GetByUsername(_properUser.Username), Times.Once);
        _userRepository.Verify(x => x.Update(_properUser), Times.Once);
    }

    #endregion

    #region Delete

    [Test]
    public void Delete_EmptyGuid_ThrowArgumentExceptionWithMessage()
    {
        var ex = Assert.ThrowsAsync<ArgumentException>(() => _userService.Delete(_improperUser.Id));

        Assert.That(ex.Message, Is.EqualTo("Please enter a valid id."));
    }

    [Test]
    public void Delete_NotExistingId_ThrowArgumentExceptionWithMessage()
    {
        Core.Entities.User returnObject = null;
        _userRepository.Setup(x => x.GetByIdAsync(_properUser.Id)).ReturnsAsync(returnObject);

        var ex = Assert.ThrowsAsync<ArgumentException>(() => _userService.Delete(_properUser.Id));
        _userRepository.Verify(x => x.GetByIdAsync(_properUser.Id), Times.Once);

        Assert.That(ex.Message, Is.EqualTo("User does not exists. Please check the entity."));
    }

    [Test]
    public void Delete_WithProperArgs_CallDeleteMethod()
    {
        _userRepository.Setup(x => x.GetByIdAsync(_properUser.Id)).ReturnsAsync(_properUser);

        _userService.Delete(_properUser.Id);

        _userRepository.Verify(x => x.GetByIdAsync(_properUser.Id), Times.Once);
        _userRepository.Verify(x => x.Delete(_properUser.Id), Times.Once);
    }

    #endregion

    #endregion
}