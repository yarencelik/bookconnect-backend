using BookConnect.Application.Common.Exceptions;
using MediatR;

namespace BookConnect.Application.Features.Reviews.Commands.DeleteReviewById;

sealed class DeleteReviewByIdCommandHandler : IRequestHandler<DeleteReviewByIdCommand>
{
    private readonly IReviewsRepository _reviewsRepository;
    public DeleteReviewByIdCommandHandler(IReviewsRepository reviewsRepository)   
    {
        _reviewsRepository = reviewsRepository;
    } 
    public async Task Handle(DeleteReviewByIdCommand request, CancellationToken cancellationToken)
    {
        var review = await _reviewsRepository.GetValue(x => x.Id.ToString() == request.reviewId)
            ?? throw new NotFoundException($"Review with ID '{request.reviewId}' was not found.");

        _reviewsRepository.Delete(review);
        await _reviewsRepository.SaveChangesAsync();
    }
}