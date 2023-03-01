using LibraryManagement.Core.Entities;
using LibraryManagement.Core.Interfaces;

namespace LibraryManagement.Infrastructure.Repositories;

public class UserRepository : GenericRepository<User>, IUserRepository
{
    private readonly LibraryContext _context;

    public UserRepository(LibraryContext context) : base(context)
    {
        _context = context;
    }
}