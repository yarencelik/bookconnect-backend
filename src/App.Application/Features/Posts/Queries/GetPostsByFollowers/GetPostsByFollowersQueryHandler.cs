using App.Application.Common.Interfaces;
using App.Application.Common.Models;
using App.Application.Features.Posts.Models;
using AutoMapper;
using MediatR;

namespace App.Application.Features.Posts.Queries.GetPostsByFollowers;

sealed class GetPostsByFollowersQueryHandler: IRequestHandler<GetPostsByFollowersQuery, PaginatedResults<PostsDetailsDto>>
{
    private readonly IPostsRepository _postsRepository;
    private readonly ICurrentUserService _currentUserService;
    private readonly IMapper _mapper;
    public GetPostsByFollowersQueryHandler(IPostsRepository postsRepository, ICurrentUserService currentUserService, IMapper mapper)
    {
        _postsRepository = postsRepository;
        _currentUserService = currentUserService;
        _mapper = mapper;
    }
    public async Task<PaginatedResults<PostsDetailsDto>> Handle(GetPostsByFollowersQuery request, CancellationToken cancellationToken)
    {
        var(results, pageData) = await _postsRepository.GetFollowersPosts(_currentUserService.UserId ?? string.Empty,request.Page, request.PageSize);

        var mappedResults = _mapper.Map<IEnumerable<PostsDetailsDto>>(results);

        return new PaginatedResults<PostsDetailsDto>(mappedResults, pageData);

    }
}