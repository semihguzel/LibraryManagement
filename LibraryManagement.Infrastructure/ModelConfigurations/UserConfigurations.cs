using LibraryManagement.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace LibraryManagement.Infrastructure.ModelConfigurations;

public class UserConfigurations : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        builder.HasKey(x => x.Id);
        builder.Property(x => x.CreatedDate).HasColumnType("timestamp");

        builder.Property(x => x.Username).IsRequired().HasMaxLength(100);
        builder.Property(x => x.Phone).IsRequired().HasMaxLength(17);
        builder.Property(x => x.Email).IsRequired().HasMaxLength(50);

        builder.HasMany(x => x.UserRoles).WithMany(x => x.Users);
    }
}