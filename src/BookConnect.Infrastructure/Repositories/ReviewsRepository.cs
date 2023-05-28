using App.Application.Features.Reviews;
using App.Domain.Entities;
using App.Infrastructure.Persistence;

namespace App.Infrastructure.Repositories;
public class ReviewsRepository : RepositoryBase<Review>, IReviewsRepository
{
    public ReviewsRepository(ApplicationDbContext context) : base(context)
    {
    }
}
