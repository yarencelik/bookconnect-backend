using System.Linq.Expressions;
using App.Application.Common.Models;
using App.Application.Features.Posts.Models;
using App.Domain.Entities;
using AutoMapper;
using MediatR;

namespace App.Application.Features.Posts.Queries;

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
