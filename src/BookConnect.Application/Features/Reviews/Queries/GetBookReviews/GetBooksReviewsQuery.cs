using BookConnect.Application.Common.Models;
using BookConnect.Application.Features.Reviews.Models;

namespace BookConnect.Application.Features.Reviews.Queries.GetBookReviews;

public class GetBookReviewsQuery : BaseQuery<ReviewDetailsDto>
{
    public required string BookId { get; set; }
    public bool IncludeReviewer { get; set; }
    public bool IncludeBook { get; set; }
}