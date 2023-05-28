using System.Linq.Expressions;
using App.Application.Common.Exceptions;
using App.Application.Features.Reviews.Models;
using App.Domain.Entities;
using AutoMapper;
using MediatR;

namespace App.Application.Features.Reviews.Queries;
public record GetReviewByIdQuery(string reviewId) : IRequest<ReviewDetailsDto>;


sealed class GetReviewByIdQueryHandler : IRequestHandler<GetReviewByIdQuery, ReviewDetailsDto>
{
    private readonly IReviewsRepository _reviewsRepository;
    private readonly IMapper _mapper;
    public GetReviewByIdQueryHandler(IReviewsRepository reviewsRepository, IMapper mapper)
    {
       _reviewsRepository = reviewsRepository;
       _mapper = mapper;
    }
    public async Task<ReviewDetailsDto> Handle(GetReviewByIdQuery request, CancellationToken cancellationToken)
    {
        var reviews = await _reviewsRepository.GetValue(
            x => x.Id.ToString() == request.reviewId, 
            new List<Expression<Func<Review, object>>>
            {
                x => x.Book!,
                x => x.Reviewer!
            }) ?? throw new NotFoundException($"Review with the ID: {request.reviewId} was not found.");


        var mappedReviews = _mapper.Map<ReviewDetailsDto>(reviews);

        return mappedReviews;
    }
}
