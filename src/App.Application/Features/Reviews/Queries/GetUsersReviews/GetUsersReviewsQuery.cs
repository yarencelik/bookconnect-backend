using App.Application.Common.Models;
using App.Application.Features.Reviews.Models;

namespace App.Application.Features.Reviews.Queries.GetUsersReviews;

public class GetUsersReviewsQuery : BaseQuery<ReviewDetailsDto>
{
    public required string UserId { get; set; }
    public bool IncludeReviewer { get; set; }
    public bool IncludeBook { get; set; }
}