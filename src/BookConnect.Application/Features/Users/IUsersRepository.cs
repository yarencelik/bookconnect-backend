using BookConnect.Application.Common.Interfaces;
using BookConnect.Application.Common.Models;
using BookConnect.Domain.Entities;

namespace BookConnect.Application.Features.Users;

public interface IUsersRepository : IRepositoryBase<User>
{
    Task<(IEnumerable<User>, PageMetadata)> GetUserFollowersOrFollowings(string userId, int page, int pageSize, bool isFollowers = true);
}
