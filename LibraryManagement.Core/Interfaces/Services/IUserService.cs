using LibraryManagement.Core.Entities;

namespace LibraryManagement.Core.Interfaces.Services;

public interface IUserService
{
    Task Add(User user);
    Task Update(User user);
    Task Delete(Guid id);
    Task<User?> GetByUsername(string name);
    Task<User?> GetByEmail(string email);
    Task<User?> GetByPhone(string phone);
}