using MediatR;

namespace BookConnect.Application.Features.Reviews.Commands.DeleteReviewById;

public record DeleteReviewByIdCommand(string reviewId) : IRequest;