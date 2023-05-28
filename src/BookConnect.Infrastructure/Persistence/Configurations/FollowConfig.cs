using BookConnect.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BookConnect.Infrastructure.Persistence.Configurations;

public class FollowConfig : IEntityTypeConfiguration<Follow>
{
    public void Configure(EntityTypeBuilder<Follow> builder)
    {
        builder
            .HasOne(x => x.Follower_User)
            .WithMany(x => x.Followings)
            .HasForeignKey(x => x.Following_Id)
            .OnDelete(DeleteBehavior.Restrict);

        builder
            .HasOne(x => x.Following_User)
            .WithMany(x => x.Followers)
            .HasForeignKey(x => x.Follower_Id)
            .OnDelete(DeleteBehavior.Restrict);
    }
}