using LibraryManagement.Core.Interfaces.Repositories;
using LibraryManagement.Core.Interfaces.Services;
using LibraryManagement.Service.User;
using LibraryManagement.Service.UserRole;
using Moq;
using NUnit.Framework;

namespace LibraryManagement.UnitTests.Service.UserRole;

[TestFixture]
public class UserRoleServiceTests
{
    private Mock<IUserRoleRepository> _userRoleRepository;
    private IUserRoleService _userRoleService;
    private Mock<IUserRepository> _userRepository;
    private IUserService _userService;

    private Core.Entities.UserRole _properUserRole;
    private Core.Entities.UserRole _improperUserRole;


    [SetUp]
    public void SetUp()
    {
        _userRepository = new Mock<IUserRepository>();
        _userService = new UserService(_userRepository.Object);
        _userRoleRepository = new Mock<IUserRoleRepository>();
        _userRoleService = new UserRoleService(_userRoleRepository.Object, _userService);

        _properUserRole = new Core.Entities.UserRole
        {
            Id = Guid.NewGuid(),
            Name = "Admin",
            CreatedDate = DateTime.Now,
            Users = new List<Core.Entities.User>
            {
                new()
                {
                    Username = "semih123",
                    Email = "email",
                    Id = Guid.NewGuid(),
                    Phone = "123432",
                    CreatedDate = DateTime.Now,
                    UserRoles = new List<Core.Entities.UserRole>
                        { new() { Id = Guid.NewGuid(), Name = "User", CreatedDate = DateTime.Now } }
                }
            }
        };
        _improperUserRole = new Core.Entities.UserRole
        {
            Id = Guid.Empty,
            Name = "       ",
            CreatedDate = DateTime.MinValue,
        };
    }

    #region Crud

    #region Add

    [Test]
    public void Add_WithNullObject_ThrowArgumentNullException()
    {
        Core.Entities.UserRole nullObj = null;

        Assert.ThrowsAsync<ArgumentNullException>(() => _userRoleService.Add(nullObj));
    }

    [Test]
    public void Add_EmptyEntity_ThrowArgumentException()
    {
        Assert.ThrowsAsync<ArgumentException>(() => _userRoleService.Add(_improperUserRole));
    }

    [Test]
    public void Add_ExistingEntity_ThrowExceptionWithMessage()
    {
        _userRoleRepository.Setup(x => x.GetByName(_properUserRole.Name)).ReturnsAsync(_properUserRole);

        var ex = Assert.ThrowsAsync<Exception>(() => _userRoleService.Add(_properUserRole));
        Assert.That(ex.Message, Is.EqualTo("Role name already exists. Please change role name and try again."));
    }

    [Test]
    public void Add_WithProperArgs_CallCreateMethod()
    {
        _userRoleService.Add(_properUserRole);
        _userRoleRepository.Verify(x => x.Create(_properUserRole), Times.Once);
    }

    #endregion

    #region Update

    [Test]
    public void Update_NullObj_ThrowArgumentNullException()
    {
        Core.Entities.UserRole nullObj = null;
        Assert.ThrowsAsync<ArgumentNullException>(() => _userRoleService.Update(nullObj));
    }

    [Test]
    public void Update_WithEmptyEntity_ThrowArgumentException()
    {
        Assert.ThrowsAsync<ArgumentException>(() => _userRoleService.Update(_improperUserRole));
    }

    [Test]
    public void Update_WithNonExistingObj_ThrowExceptionWithMessage()
    {
        Core.Entities.UserRole nullObject = null;

        _userRoleRepository.Setup(x => x.GetByName(_properUserRole.Name)).ReturnsAsync(nullObject);

        var ex = Assert.ThrowsAsync<Exception>(() => _userRoleService.Update(_properUserRole));
        Assert.That(ex.Message, Is.EqualTo("Role does not exists. Please check the entity."));
    }

    [Test]
    public void Update_WithProperArgs_CallUpdateMethod()
    {
        _userRoleRepository.Setup(x => x.GetByName(_properUserRole.Name)).ReturnsAsync(_properUserRole);

        _userRoleService.Update(_properUserRole);

        _userRoleRepository.Verify(x => x.GetByName(_properUserRole.Name), Times.Once);
        _userRoleRepository.Verify(x => x.Update(_properUserRole));
    }

    #endregion

    #region Delete

    [Test]
    public void Delete_WithEmptyGuid_ThrowArgumentExceptionWithMessage()
    {
        var ex = Assert.ThrowsAsync<ArgumentException>(() => _userRoleService.Delete(_improperUserRole.Id));

        Assert.That(ex.Message, Is.EqualTo("Please enter a valid id."));
    }

    [Test]
    public void Delete_WithNonExistingId_ThrowArgumentExceptionWithMessage()
    {
        Core.Entities.UserRole nullObj = null;
        _userRoleRepository.Setup(x => x.GetByIdAsync(_properUserRole.Id)).ReturnsAsync(nullObj);

        var ex = Assert.ThrowsAsync<ArgumentException>(() => _userRoleService.Delete(_properUserRole.Id));
        Assert.That(ex.Message, Is.EqualTo("User role couldn't have been found. Please check sent arguments."));
    }

    [Test]
    public void Delete_RoleWithUsers_ThrowNewExceptionWithMessage()
    {
        _userRepository.Setup(x => x.GetUsersByRoleId(_properUserRole.Id)).ReturnsAsync(_properUserRole.Users.ToList);
        _userRoleRepository.Setup(x => x.GetByIdAsync(_properUserRole.Id)).ReturnsAsync(_properUserRole);

        var ex = Assert.ThrowsAsync<Exception>(() => _userRoleService.Delete(_properUserRole.Id));
        
        Assert.That(ex.Message, Is.EqualTo("This role cannot be deleted as it contains one or more users."));
    }

    #endregion

    #endregion

    #region GetByName

    [Test]
    public void GetByName_WithWrongArgs_ThrowArgumentException()
    {
        Assert.ThrowsAsync<ArgumentException>(() => _userRoleService.GetByName(_improperUserRole.Name));
    }

    #endregion

    #region GetById

    [Test]
    public void GetById_WithEmptyGuid_ThrowExceptionWithMessage()
    {
        var ex = Assert.ThrowsAsync<Exception>(() => _userRoleService.GetById(_improperUserRole.Id));
        Assert.That(ex.Message, Is.EqualTo("Please enter a valid id."));
    }

    #endregion
}