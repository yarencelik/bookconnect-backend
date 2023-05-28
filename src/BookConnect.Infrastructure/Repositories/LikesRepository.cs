using BookConnect.Application.Features.Likes;
using BookConnect.Domain.Entities;
using BookConnect.Infrastructure.Persistence;

namespace BookConnect.Infrastructure.Repositories;

public sealed class LikesRepository : RepositoryBase<Likes>, ILikesRepository
{
    public LikesRepository(ApplicationDbContext context) : base(context)
    {
    }
}