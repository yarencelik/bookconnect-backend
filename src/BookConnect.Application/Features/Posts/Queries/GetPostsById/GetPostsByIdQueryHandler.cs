using System.Linq.Expressions;
using App.Application.Common.Exceptions;
using App.Application.Features.Posts.Models;
using App.Domain.Entities;
using AutoMapper;
using MediatR;

namespace App.Application.Features.Posts.Queries.GetPostsById;

sealed class GetPostByIdQueryHandler : IRequestHandler<GetPostByIdQuery, PostsDetailsDto>
{
    private readonly IPostsRepository _postsRepository;
    private readonly IMapper _mapper;
    public GetPostByIdQueryHandler(IPostsRepository postsRepository, IMapper mapper)
    {
        _postsRepository = postsRepository;   
        _mapper = mapper;
    }
    public async Task<PostsDetailsDto> Handle(GetPostByIdQuery request, CancellationToken cancellationToken)
    {
        var post = await _postsRepository.GetValue(
            x => x.Id.ToString() == request.postId,
            new List<Expression<Func<Post, object>>>
            {
                x => x.Owner
            }
        ); 

        if(post == null)       
            throw new NotFoundException($"Post with Id '{request.postId}'was not found.");

        var mappedPost = _mapper.Map<PostsDetailsDto>(post);

        return mappedPost;
    }
}
