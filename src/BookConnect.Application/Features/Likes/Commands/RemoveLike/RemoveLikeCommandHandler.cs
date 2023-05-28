using System.Linq.Expressions;
using LikesEntity = BookConnect.Domain.Entities.Likes;
using MediatR;
using BookConnect.Application.Common.Interfaces;
using BookConnect.Application.Features.Users;
using BookConnect.Application.Features.Posts;
using BookConnect.Application.Common.Exceptions;

namespace BookConnect.Application.Features.Likes.Commands.RemoveLike;

sealed class RemoveLikeCommandHandler :  IRequestHandler<RemoveLikeCommand>
{
    private readonly ICurrentUserService _currentUserService;
    private readonly IUsersRepository _usersRepository;
    private readonly IPostsRepository _postsRepository;
    private readonly ILikesRepository _likesRepository;
    public RemoveLikeCommandHandler(ILikesRepository likesRepository, IPostsRepository postsRepository, IUsersRepository usersRepository, ICurrentUserService currentUserService)
    {
        _likesRepository = likesRepository;
        _postsRepository = postsRepository;
        _usersRepository = usersRepository;
        _currentUserService = currentUserService;
    }
    public async Task Handle(RemoveLikeCommand request, CancellationToken cancellationToken)
    {
        var post = await _postsRepository.GetValue(x => x.Id.ToString() == request.postId)
            ?? throw new NotFoundException($"Post with Id '{request.postId}' was not found.");

        var user = await _usersRepository.GetValue(x => x.Id.ToString() == _currentUserService.UserId)
             ?? throw new UnauthorizedAccessException();

        var likes = await _likesRepository.GetValue(
            x => x.Like_Post_Id == post.Id &&
            x.Like_User_Id == user.Id,
            new List<Expression<Func<LikesEntity, object>>>
            {
                x => x.Like_Post,
                x => x.Like_User
            }
            )
            ?? throw new NotFoundException("Likes record was not found");

        _likesRepository.Delete(likes);
        await _likesRepository.SaveChangesAsync();
    }
}