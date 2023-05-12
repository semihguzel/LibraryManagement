using LibraryManagement.Core.Entities;

namespace LibraryManagement.Core.Interfaces.Repositories;

public interface IUserRoleRepository : IRepository<UserRole>
{
    Task<UserRole?> GetByName(string name);
}