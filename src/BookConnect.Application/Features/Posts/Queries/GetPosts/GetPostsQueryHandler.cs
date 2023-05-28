using System.Linq.Expressions;
using App.Application.Common.Models;
using App.Application.Features.Posts.Models;
using App.Domain.Entities;
using AutoMapper;
using MediatR;

namespace App.Application.Features.Posts.Queries.GetPosts;

sealed class GetPostsQueryHandler : IRequestHandler<GetPostsQuery, PaginatedResults<PostsDetailsDto>>
{
    private readonly IPostsRepository _postsRepository;
    private readonly IMapper _mapper;
    public GetPostsQueryHandler(IPostsRepository postsRepository, IMapper mapper)
    {
        _postsRepository = postsRepository;   
        _mapper = mapper;
    }
    public async Task<PaginatedResults<PostsDetailsDto>> Handle(GetPostsQuery request, CancellationToken cancellationToken)
    {
        var (results, pageData) = await _postsRepository.GetAllValuesPaginated(
            request.Page, 
            request.PageSize,
            null,
            new List<Expression<Func<Post, object>>>
            {
                x => x.Owner
            });

        var mapped = _mapper.Map<IEnumerable<PostsDetailsDto>>(results);

        return new PaginatedResults<PostsDetailsDto>(mapped, pageData);
    }
}
