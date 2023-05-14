namespace LibraryManagement.Service.Book.Dto;

public class BookCreateUpdateDto
{
    public string Name { get; set; }
    public string Author { get; set; }
    public int PageNumber { get; set; }
    public int Quantity { get; set; }

    public Guid BookCategoryId { get; set; }
}