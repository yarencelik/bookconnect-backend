using App.Application.Common.Interfaces;
using App.Application.Features.Follow.Models;
using FollowEntity = App.Domain.Entities.Follow;

namespace App.Application.Features.Follow;

public interface IFollowRepository : IRepositoryBase<FollowEntity>
{
    Task<FollowersAndFollowingsDto> GetUsersFollowsCount(string userId);
}