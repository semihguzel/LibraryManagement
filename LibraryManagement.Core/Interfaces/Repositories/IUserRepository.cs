using LibraryManagement.Core.Entities;

namespace LibraryManagement.Core.Interfaces.Repositories;

public interface IUserRepository : IRepository<User>
{
    Task<User?> GetByUsername(string name);
    Task<User?> GetByEmail(string email);
    Task<User?> GetByPhone(string phone);
    Task<List<User?>> GetUsersByRoleId(Guid roleId);
}