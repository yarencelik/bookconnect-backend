using System.Linq.Expressions;
using App.Application.Common.Models;
using App.Application.Features.Reviews.Models;
using App.Domain.Entities;
using AutoMapper;
using MediatR;

namespace App.Application.Features.Reviews.Queries.GetBooksReviews;

sealed class GetBookReviewsQueryHandler : IRequestHandler<GetBookReviewsQuery, PaginatedResults<ReviewDetailsDto>>
{
    private readonly IReviewsRepository _reviewsRepository;
    private readonly IMapper _mapper;
    public GetBookReviewsQueryHandler(IReviewsRepository reviewsRepository, IMapper mapper)
    {
       _reviewsRepository = reviewsRepository;
       _mapper = mapper; 
    }
    public async Task<PaginatedResults<ReviewDetailsDto>> Handle(GetBookReviewsQuery request, CancellationToken cancellationToken)
    {
        var includes = new List<Expression<Func<Review, object>>>();

        if(request.IncludeBook)
            includes.Add(x => x.Book!);

        if(request.IncludeReviewer)        
            includes.Add(x => x.Reviewer!);

        var (results, pageData) = await _reviewsRepository.GetAllValuesPaginated(
            request.Page,
            request.PageSize,
            x => x.Book_Id.ToString() == request.BookId,
            includes
        );

        var mappedResults = _mapper.Map<IEnumerable<ReviewDetailsDto>>(results);

        return new PaginatedResults<ReviewDetailsDto>(mappedResults, pageData);
    }
}