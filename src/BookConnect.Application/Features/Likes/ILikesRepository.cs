using App.Application.Common.Interfaces;
using LikesEntity = App.Domain.Entities.Likes;

namespace App.Application.Features.Likes;

public interface ILikesRepository : IRepositoryBase<LikesEntity>
{
    
}