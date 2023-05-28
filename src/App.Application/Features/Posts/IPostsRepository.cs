using App.Application.Common.Interfaces;
using App.Application.Common.Models;
using App.Domain.Entities;

namespace App.Application.Features.Posts;
public interface IPostsRepository : IRepositoryBase<Post>
{
    Task<(IEnumerable<Post>, PageMetadata)> GetFollowersPosts(string followerId, int page = 1, int pageSize = 10);
}