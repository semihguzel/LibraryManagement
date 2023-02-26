namespace LibraryManagement.Core.Entities;

public class BookCategory : BaseEntity
{
    public string Code { get; set; }
    public string Description { get; set; }

    public ICollection<Book> Books { get; set; }
}