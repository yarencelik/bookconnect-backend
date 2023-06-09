﻿using BookConnect.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BookConnect.Infrastructure.Persistence.Configurations;

public class BookConfig : IEntityTypeConfiguration<Book>
{
    public void Configure(EntityTypeBuilder<Book> builder)
    {
        builder
            .HasMany(x => x.Reviews)
            .WithOne(x => x.Book)
            .HasForeignKey(x => x.Book_Id);
    }
}