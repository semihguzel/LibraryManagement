using LibraryManagement.Core.Entities;

namespace LibraryManagement.Core.Interfaces.Services;

public interface IUserRoleService
{
    Task<UserRole?> GetById(Guid id);
    Task Add(UserRole userRole);
    Task Update(UserRole userRole);
    Task Delete(Guid id);
    Task<UserRole?> GetByName(string name);
}