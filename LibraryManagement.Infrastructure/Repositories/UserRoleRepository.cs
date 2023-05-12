using LibraryManagement.Core.Entities;
using LibraryManagement.Core.Interfaces;
using LibraryManagement.Core.Interfaces.Repositories;
using Microsoft.EntityFrameworkCore;

namespace LibraryManagement.Infrastructure.Repositories;

public class UserRoleRepository : GenericRepository<UserRole>, IUserRoleRepository
{
    private readonly LibraryContext _context;

    public UserRoleRepository(LibraryContext context) : base(context)
    {
        _context = context;
    }

    public async Task<UserRole?> GetByName(string name)
    {
        return await _context.UserRoles.FirstOrDefaultAsync(x => x.Name == name);
    }
}