using BookConnect.Application.Common.Interfaces;
using LikesEntity = BookConnect.Domain.Entities.Likes;

namespace BookConnect.Application.Features.Likes;

public interface ILikesRepository : IRepositoryBase<LikesEntity>
{
    
}