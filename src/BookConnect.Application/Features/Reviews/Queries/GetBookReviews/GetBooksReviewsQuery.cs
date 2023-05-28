using App.Application.Common.Models;
using App.Application.Features.Reviews.Models;
using MediatR;

namespace App.Application.Features.Reviews.Queries.GetBooksReviews;

public class GetBookReviewsQuery : BaseQuery<ReviewDetailsDto>
{
    public required string BookId { get; set; }
    public bool IncludeReviewer { get; set; }
    public bool IncludeBook { get; set; }
}