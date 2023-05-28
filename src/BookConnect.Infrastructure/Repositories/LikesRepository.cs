using App.Application.Features.Likes;
using App.Domain.Entities;
using App.Infrastructure.Persistence;

namespace App.Infrastructure.Repositories;

public sealed class LikesRepository : RepositoryBase<Likes>, ILikesRepository
{
    public LikesRepository(ApplicationDbContext context) : base(context)
    {
    }
}