using MediatR;

namespace App.Application.Features.Reviews.Commands.DeleteReviewById;

public record DeleteReviewByIdCommand(string reviewId) : IRequest;