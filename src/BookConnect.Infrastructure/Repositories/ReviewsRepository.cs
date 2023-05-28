using BookConnect.Application.Features.Reviews;
using BookConnect.Domain.Entities;
using BookConnect.Infrastructure.Persistence;

namespace BookConnect.Infrastructure.Repositories;

public class ReviewsRepository : RepositoryBase<Review>, IReviewsRepository
{
    public ReviewsRepository(ApplicationDbContext context) : base(context)
    {
    }
}
