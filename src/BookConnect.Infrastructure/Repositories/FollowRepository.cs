using BookConnect.Application.Features.Follow;
using BookConnect.Application.Features.Follow.Models;
using BookConnect.Domain.Entities;
using BookConnect.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace BookConnect.Infrastructure.Repositories;

public class FollowRepository : RepositoryBase<Follow>, IFollowRepository
{
    public FollowRepository(ApplicationDbContext context) : base(context)
    {
    }

    public async Task<FollowersAndFollowingsDto> GetUsersFollowsCount(string userId)
    {
         IQueryable<Follow> query = _context.Set<Follow>();

        // Count of User's Followers
        var followersCount = await query
            .AsNoTracking()
            .CountAsync(x => x.Following_Id.ToString() == userId);

        // Count of Users Followed
        var followingsCount = await query
            .AsNoTracking()
            .CountAsync(x => x.Follower_Id.ToString() == userId);


        return new FollowersAndFollowingsDto
        {
            TotalFollowers = followersCount,
            TotalFollowings = followingsCount
        };
    }
}