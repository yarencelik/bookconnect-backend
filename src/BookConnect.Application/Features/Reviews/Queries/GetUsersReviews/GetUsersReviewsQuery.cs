using BookConnect.Application.Common.Models;
using BookConnect.Application.Features.Reviews.Models;

namespace BookConnect.Application.Features.Reviews.Queries.GetUsersReviews;

public class GetUsersReviewsQuery : BaseQuery<ReviewDetailsDto>
{
    public required string UserId { get; set; }
    public bool IncludeReviewer { get; set; }
    public bool IncludeBook { get; set; }
}