using App.Application.Common.Interfaces;
using App.Application.Features.Users;
using App.Domain.Entities;
using AutoMapper;
using MediatR;

namespace App.Application.Features.Posts.Commands.CreatePost;

sealed class CreatePostCommandHandler : IRequestHandler<CreatePostCommand, Guid>
{
    private readonly ICurrentUserService _currentUserService;
    private readonly IUsersRepository _usersRepository;
    private readonly IPostsRepository _postRepository;
    private readonly IMapper _mapper;
    public CreatePostCommandHandler(IPostsRepository postRepository, IUsersRepository usersRepository, IMapper mapper, ICurrentUserService currentUserService)
    {
        _usersRepository = usersRepository;
        _postRepository = postRepository;
        _mapper = mapper;
        _currentUserService = currentUserService;
    }
    public async Task<Guid> Handle(CreatePostCommand request, CancellationToken cancellationToken)
    {
        var user = await _usersRepository.GetValue(x => x.Id.ToString() == _currentUserService.UserId)
            ?? throw new UnauthorizedAccessException();

        var newPost = _mapper.Map<Post>(request);
        newPost.OwnerId = user.Id;

        await _postRepository.Create(newPost);
        await _postRepository.SaveChangesAsync();

        return newPost.Id;
    }
}