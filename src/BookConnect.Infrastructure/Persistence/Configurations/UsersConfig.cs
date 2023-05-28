using BookConnect.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;


namespace BookConnect.Infrastructure.Persistence.Configurations;

public class UsersConfig : IEntityTypeConfiguration<User>
{

    public void Configure(EntityTypeBuilder<User> builder)
    {
        //  User - Author Config
        builder
            .HasOne(x => x.Author)
            .WithOne(x => x.User)
            .HasForeignKey<Author>(x => x.UserId);

        // User - Posts config
        builder
            .HasMany(x => x.Posts)
            .WithOne(x => x.Owner)
            .HasForeignKey(x => x.OwnerId);

        // User - Likes config
        builder
            .HasMany(x => x.LikedPost)
            .WithOne(x => x.Like_User)
            .HasForeignKey(x => x.Like_User_Id)
            .OnDelete(DeleteBehavior.SetNull);


        builder
            .HasMany(x => x.BookReviews)
            .WithOne(x => x.Reviewer)
            .HasForeignKey(x => x.Reviewer_Id);
    }
}
