namespace LibraryManagement.Core.Entities;

public class Book : BaseEntity
{
    public string Name { get; set; }
    public string Author { get; set; }
    public int PageNumber { get; set; }
    public int Quantity { get; set; }

    public ICollection<BookCategory> BookCategories { get; set; }
    public ICollection<Loan> Loans { get; set; }
}