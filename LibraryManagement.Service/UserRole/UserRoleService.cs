using LibraryManagement.Core.Helpers.UserRoleHelpers;
using LibraryManagement.Core.Interfaces.Repositories;
using LibraryManagement.Core.Interfaces.Services;

namespace LibraryManagement.Service.UserRole;

public class UserRoleService : IUserRoleService
{
    private readonly IUserRoleRepository _userRoleRepository;
    private readonly IUserService _userService;

    public UserRoleService(IUserRoleRepository userRoleRepository, IUserService userService)
    {
        _userRoleRepository = userRoleRepository;
        _userService = userService;
    }

    public async Task Add(Core.Entities.UserRole userRole)
    {
        UserRoleServiceHelper.CheckArgsForException(userRole);

        var doesExists = await _userRoleRepository.GetByName(userRole.Name) != null;

        if (doesExists)
            throw new Exception("Role name already exists. Please change role name and try again.");

        await _userRoleRepository.Create(userRole);
    }

    public async Task Update(Core.Entities.UserRole userRole)
    {
        UserRoleServiceHelper.CheckArgsForException(userRole);

        var doesExists = await _userRoleRepository.GetByName(userRole.Name) != null;

        if (!doesExists)
            throw new Exception("Role does not exists. Please check the entity.");

        await _userRoleRepository.Update(userRole);
    }

    public async Task Delete(Guid id)
    {
        if (id == Guid.Empty)
            throw new ArgumentException("Please enter a valid id.");

        var doesExists = await _userRoleRepository.GetByIdAsync(id) != null;

        if (!doesExists)
            throw new ArgumentException("User role couldn't have been found. Please check sent arguments.");

        var doesHaveUsers = await _userService.GetUsersByRoleId(id);

        if (doesHaveUsers != null && doesHaveUsers.Count > 0)
            throw new Exception("This role cannot be deleted as it contains one or more users.");

        await _userRoleRepository.Delete(id);
    }

    public async Task<Core.Entities.UserRole?> GetByName(string name)
    {
        if (string.IsNullOrWhiteSpace(name))
            throw new ArgumentException();

        return await _userRoleRepository.GetByName(name);
    }

    public async Task<Core.Entities.UserRole?> GetById(Guid id)
    {
        if (id == Guid.Empty)
            throw new Exception("Please enter a valid id.");
        
        return await _userRoleRepository.GetByIdAsync(id);
    }
}