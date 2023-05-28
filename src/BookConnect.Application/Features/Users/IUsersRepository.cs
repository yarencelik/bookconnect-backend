using App.Application.Common.Interfaces;
using App.Application.Common.Models;
using App.Domain.Entities;

namespace App.Application.Features.Users;

public interface IUsersRepository : IRepositoryBase<User>
{
    Task<(IEnumerable<User>, PageMetadata)> GetUserFollowersOrFollowings(string userId, int page, int pageSize, bool isFollowers = true);
}
