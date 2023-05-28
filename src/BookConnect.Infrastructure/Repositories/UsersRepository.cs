using App.Application.Common.Models;
using App.Application.Features.Users;
using App.Domain.Entities;
using App.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace App.Infrastructure.Repositories;
public class UsersRepository : RepositoryBase<User>, IUsersRepository
{
    public UsersRepository(ApplicationDbContext context): base(context)
    {

    }

    public async Task<(IEnumerable<User>, PageMetadata)> GetUserFollowersOrFollowings(string userId, int page, int pageSize, bool isFollowers = true)
    {
        IQueryable<User> userQuery = _context.Set<User>();  
        IQueryable<Follow> followQuery = _context.Set<Follow>();

        userQuery = userQuery 
            .AsNoTracking()
            .OrderByDescending(x => x.UpdatedAt)
            .ThenByDescending(x => x.CreatedAt)
            .Skip(pageSize * (page - 1))
            .Take(pageSize);

        var userFollowsOrFollowings = followQuery
            .AsNoTracking()
            .Where(x => isFollowers ? x.Following_Id.ToString() == userId : x.Follower_Id.ToString() == userId)
            .Select(x => new
            {
                x.Follower_Id,
                x.Following_Id
            })
            .Join(userQuery, f => isFollowers ? f.Follower_Id : f.Following_Id, p => p.Id, (f, p) => new User 
            {
                Id = p.Id,
                Username = p.Username,
                Password = p.Password,
                Email = p.Email,
                FirstName = p.FirstName,
                LastName = p.LastName,
                UpdatedAt = p.UpdatedAt,
                CreatedAt = p.CreatedAt
            });

        var totalAuthorsCount = await userFollowsOrFollowings.AsNoTracking().CountAsync(); 

        var pageData = new PageMetadata(page, pageSize, totalAuthorsCount);

        var results = await userFollowsOrFollowings 
            .AsNoTracking()
            .OrderByDescending(x => x.UpdatedAt)
            .ThenByDescending(x => x.CreatedAt)
            .Skip(pageSize * (page - 1))
            .Take(pageSize)
            .ToListAsync();

        
        return (results, pageData);
    }
}
