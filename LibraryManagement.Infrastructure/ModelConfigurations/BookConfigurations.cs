using LibraryManagement.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace LibraryManagement.Infrastructure.ModelConfigurations;

public class BookConfigurations : IEntityTypeConfiguration<Book>
{
    public void Configure(EntityTypeBuilder<Book> builder)
    {
        builder.HasKey(x => x.Id);
        builder.Property(x => x.CreatedDate).HasColumnType("timestamp");

        builder
            .Property(x => x.Name)
            .IsRequired()
            .HasMaxLength(350);

        builder
            .Property(x => x.Author)
            .IsRequired()
            .HasMaxLength(250);

        builder.Property(x => x.PageNumber).IsRequired();

        builder.HasMany(x => x.BookCategories).WithMany(x => x.Books);
    }
}