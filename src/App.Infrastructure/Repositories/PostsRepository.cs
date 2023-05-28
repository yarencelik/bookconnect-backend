using App.Application.Common.Models;
using App.Application.Features.Posts;
using App.Domain.Entities;
using App.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace App.Infrastructure.Repositories;

public class PostsRepository : RepositoryBase<Post>, IPostsRepository
{
    public PostsRepository(ApplicationDbContext context) : base(context)
    {
    }

    public async Task<(IEnumerable<Post>, PageMetadata)> GetFollowersPosts(string followerId, int page = 1, int pageSize = 10)
    {
        IQueryable<Post> postQuery = _context.Set<Post>();  
        IQueryable<Follow> query = _context.Set<Follow>();

        postQuery = postQuery
            .AsNoTracking()
            .Include(x => x.Owner)
            .OrderByDescending(x => x.UpdatedAt)
            .ThenByDescending(x => x.CreatedAt)
            .Skip(pageSize * (page - 1))
            .Take(pageSize);

        var followersPosts = query
            .AsNoTracking()
            .Select(x => new
            {
                x.Follower_Id,
                x.Following_Id
            })
            .Where(x => x.Follower_Id.ToString() == followerId)
            .Join(postQuery, f => f.Following_Id, p => p.OwnerId, (f, p) => new Post
            {

                Id = p.Id,
                Title = p.Title,
                Body = p.Body,
                Owner = p.Owner,
                UpdatedAt = p.UpdatedAt,
                CreatedAt = p.CreatedAt
            });

        var totalAuthorsCount = await followersPosts.AsNoTracking().CountAsync(); 

        var pageData = new PageMetadata(page, pageSize, totalAuthorsCount);

        var results = await followersPosts
            .AsNoTracking()
            .OrderByDescending(x => x.UpdatedAt)
            .ThenByDescending(x => x.CreatedAt)
            .Skip(pageSize * (page - 1))
            .Take(pageSize)
            .ToListAsync();

        
        return (results, pageData);
    }
}