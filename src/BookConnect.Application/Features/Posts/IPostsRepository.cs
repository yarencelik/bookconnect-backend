using BookConnect.Application.Common.Interfaces;
using BookConnect.Application.Common.Models;
using BookConnect.Domain.Entities;

namespace BookConnect.Application.Features.Posts;

public interface IPostsRepository : IRepositoryBase<Post>
{
    Task<(IEnumerable<Post>, PageMetadata)> GetFollowersPosts(string followerId, int page = 1, int pageSize = 10);
}