using System.Linq.Expressions;
using LikesEntity = BookConnect.Domain.Entities.Likes;
using MediatR;
using BookConnect.Application.Common.Interfaces;
using BookConnect.Application.Features.Users;
using BookConnect.Application.Features.Posts;
using BookConnect.Application.Common.Exceptions;

namespace BookConnect.Application.Features.Likes.Commands.AddLike;

sealed class AddLikeCommandHandler : IRequestHandler<AddLikeCommand>
{
    private readonly ICurrentUserService _currentUserService;
    private readonly IUsersRepository _usersRepository;
    private readonly IPostsRepository _postsRepository;
    private readonly ILikesRepository _likesRepository;
    public AddLikeCommandHandler(ILikesRepository likesRepository, IPostsRepository postsRepository, IUsersRepository usersRepository, ICurrentUserService currentUserService)
    {
        _likesRepository = likesRepository;
        _postsRepository = postsRepository;
        _usersRepository = usersRepository;
        _currentUserService = currentUserService;
    }
    public async Task Handle(AddLikeCommand request, CancellationToken cancellationToken)
    {
        var post = await _postsRepository.GetValue(x => x.Id.ToString() == request.postId, null, false)
            ?? throw new NotFoundException($"Post with Id '{request.postId}' was not found.");

        var user = await _usersRepository.GetValue(x => x.Id.ToString() == _currentUserService.UserId, null, false)
            ?? throw new UnauthorizedAccessException();

        // Check if user already liked the existing post
        var likes = await _likesRepository.GetValue(
            x => x.Like_Post_Id == post.Id &&
            x.Like_User_Id == user.Id,
            new List<Expression<Func<LikesEntity, object>>>
            {
                x => x.Like_Post,
                x => x.Like_User
            }, false
            );
        if(likes != null)
            throw new ConflictException($"User already liked this Post with Id '{post.Id}'");

        
        var newLikes = new LikesEntity
        {
            Like_Post = post,
            Like_User= user
        };

        await _likesRepository.Create(newLikes);
        await _likesRepository.SaveChangesAsync();
    }
}
