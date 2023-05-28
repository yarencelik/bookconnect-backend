using System.Linq.Expressions;
using AutoMapper;
using BookConnect.Application.Common.Models;
using BookConnect.Application.Features.Reviews.Models;
using BookConnect.Domain.Entities;
using MediatR;

namespace BookConnect.Application.Features.Reviews.Queries.GetUsersReviews;

sealed class GetUsersReviewsQueryHandler : IRequestHandler<GetUsersReviewsQuery, PaginatedResults<ReviewDetailsDto>>
{
    private readonly IReviewsRepository _reviewsRepository;
    private readonly IMapper _mapper;
    public GetUsersReviewsQueryHandler(IReviewsRepository reviewsRepository, IMapper mapper)
    {
       _reviewsRepository = reviewsRepository;
       _mapper = mapper; 
    }
    public async Task<PaginatedResults<ReviewDetailsDto>> Handle(GetUsersReviewsQuery request, CancellationToken cancellationToken)
    {
        var includes = new List<Expression<Func<Review, object>>>();

        if(request.IncludeBook)
            includes.Add(x => x.Book!);

        if(request.IncludeReviewer)        
            includes.Add(x => x.Reviewer!);

        var (results, pageData) = await _reviewsRepository.GetAllValuesPaginated(
            request.Page,
            request.PageSize,
            x => x.Reviewer_Id.ToString() == request.UserId,
            includes
        );

        var mappedResults = _mapper.Map<IEnumerable<ReviewDetailsDto>>(results);

        return new PaginatedResults<ReviewDetailsDto>(mappedResults, pageData);
    }
}