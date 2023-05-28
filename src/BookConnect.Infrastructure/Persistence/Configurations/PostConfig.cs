using BookConnect.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BookConnect.Infrastructure.Persistence.Configurations;

public class PostConfig : IEntityTypeConfiguration<Post>
{
    public void Configure(EntityTypeBuilder<Post> builder)
    {
        builder
            .HasMany(x => x.Likes)
            .WithOne(x => x.Like_Post)
            .HasForeignKey(x => x.Like_Post_Id);
    }
}