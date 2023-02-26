namespace LibraryManagement.Core.Entities;

public class Loan : BaseEntity
{
    public Guid BookId { get; set; }
    public Guid UserId { get; set; }
    public DateTime LentDate { get; set; }
    public DateTime DueDate { get; set; }
    public DateTime? ReceivedDate { get; set; }
    public User User { get; set; }
    public Book Book { get; set; }
}