using BookConnect.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BookConnect.Infrastructure.Persistence.Configurations;

public class BookShelfConfig : IEntityTypeConfiguration<BookShelf>
{
    public void Configure(EntityTypeBuilder<BookShelf> builder)
    {
        builder
            .HasOne(x => x.Book)
            .WithMany(x => x.BookShelves)
            .HasForeignKey(x => x.BookId);

        builder
            .HasOne(x => x.Shelf)
            .WithMany(x => x.BookShelves)
            .HasForeignKey(x => x.ShelfId);
    }
}