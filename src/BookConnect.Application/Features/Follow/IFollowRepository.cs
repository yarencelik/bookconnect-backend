using BookConnect.Application.Common.Interfaces;
using BookConnect.Application.Features.Follow.Models;
using FollowEntity = BookConnect.Domain.Entities.Follow;

namespace BookConnect.Application.Features.Follow;

public interface IFollowRepository : IRepositoryBase<FollowEntity>
{
    Task<FollowersAndFollowingsDto> GetUsersFollowsCount(string userId);
}