using LibraryManagement.Core.Entities;
using LibraryManagement.Core.Interfaces;
using LibraryManagement.Core.Interfaces.Repositories;

namespace LibraryManagement.Infrastructure.Repositories;

public class UserRoleRepository : GenericRepository<UserRole>, IUserRoleRepository
{
    private readonly LibraryContext _context;

    public UserRoleRepository(LibraryContext context) : base(context)
    {
        _context = context;
    }
}