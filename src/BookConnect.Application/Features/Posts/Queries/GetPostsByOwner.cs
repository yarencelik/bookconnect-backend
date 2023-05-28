using System.Linq.Expressions;
using AutoMapper;
using BookConnect.Application.Common.Models;
using BookConnect.Application.Features.Posts.Models;
using BookConnect.Domain.Entities;
using MediatR;

namespace BookConnect.Application.Features.Posts.Queries;

public class GetPostsByOwnerIdQuery : BaseQuery<PostsDetailsDto>
{
   public required string OwnerId { get; set; } 
}

sealed class GetPostsByOwnerIdQueryHandler : IRequestHandler<GetPostsByOwnerIdQuery, PaginatedResults<PostsDetailsDto>>
{
    private readonly IPostsRepository _postsRepository;
    private readonly IMapper _mapper;
    public GetPostsByOwnerIdQueryHandler(IPostsRepository postsRepository, IMapper mapper)
    {
        _postsRepository = postsRepository;
        _mapper = mapper;
    }
    public async Task<PaginatedResults<PostsDetailsDto>> Handle(GetPostsByOwnerIdQuery request, CancellationToken cancellationToken)
    {
        var (results, pageData) = await _postsRepository.GetAllValuesPaginated(
            request.Page, 
            request.PageSize, 
            x => x.OwnerId.ToString() == request.OwnerId,
            new List<Expression<Func<Post, object>>>
            {
                x => x.Owner
            });

       var mappedResults = _mapper.Map<IEnumerable<PostsDetailsDto>>(results);

       return new PaginatedResults<PostsDetailsDto>(mappedResults, pageData);
    }
}
