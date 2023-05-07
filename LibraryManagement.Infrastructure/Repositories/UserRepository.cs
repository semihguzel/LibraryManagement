using LibraryManagement.Core.Entities;
using LibraryManagement.Core.Interfaces;
using LibraryManagement.Core.Interfaces.Repositories;
using Microsoft.EntityFrameworkCore;

namespace LibraryManagement.Infrastructure.Repositories;

public class UserRepository : GenericRepository<User>, IUserRepository
{
    private readonly LibraryContext _context;

    public UserRepository(LibraryContext context) : base(context)
    {
        _context = context;
    }

    public async Task<User?> GetByUsername(string name)
    {
        return await _context.Users.FirstOrDefaultAsync(x => x.Username == name);
    }

    public async Task<User?> GetByEmail(string email)
    {
        return await _context.Users.FirstOrDefaultAsync(x => x.Email == email);
    }

    public async Task<User?> GetByPhone(string phone)
    {
        return await _context.Users.FirstOrDefaultAsync(x => x.Phone == phone);
    }
}