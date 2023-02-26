namespace LibraryManagement.Core.Entities;

public class User : BaseEntity
{
    public string Username { get; set; }
    public string Email { get; set; }
    public string Phone { get; set; }

    public ICollection<UserRole> UserRoles { get; set; }
    public ICollection<Loan> Loans { get; set; }
}