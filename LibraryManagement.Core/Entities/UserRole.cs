namespace LibraryManagement.Core.Entities;

public class UserRole : BaseEntity
{
    public string Name { get; set; }

    public ICollection<User> Users { get; set; }
}