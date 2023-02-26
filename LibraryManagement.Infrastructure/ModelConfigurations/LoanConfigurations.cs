using LibraryManagement.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace LibraryManagement.Infrastructure.ModelConfigurations;

public class LoanConfigurations : IEntityTypeConfiguration<Loan>
{
    public void Configure(EntityTypeBuilder<Loan> builder)
    {
        builder.HasKey(x => x.Id);
        builder.Property(x => x.CreatedDate).HasColumnType("timestamp");

        builder.Property(x => x.DueDate).IsRequired().HasColumnType("timestamp");
        builder.Property(x => x.LentDate).IsRequired().HasColumnType("timestamp");
        builder.Property(x => x.ReceivedDate).HasColumnType("timestamp");

        builder.HasOne(x => x.Book).WithMany(x => x.Loans).HasForeignKey(x => x.BookId);
        builder.HasOne(x => x.User).WithMany(x => x.Loans).HasForeignKey(x => x.UserId);
    }
}