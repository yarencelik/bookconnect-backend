using App.Application.Common.Exceptions;
using MediatR;

namespace App.Application.Features.Posts.Commands.DeletePost;

sealed class DeletePostCommandHandler : IRequestHandler<DeletePostCommand>
{
    private readonly IPostsRepository _postsRepository;
    public DeletePostCommandHandler(IPostsRepository postsRepository)
    {
        _postsRepository = postsRepository;
    }
    public async Task Handle(DeletePostCommand request, CancellationToken cancellationToken)
    {
        var post = await _postsRepository.GetValue(x => x.Id.ToString() == request.postId)
            ?? throw new NotFoundException($"Post with Id '{request.postId}' was not found.");
        
        _postsRepository.Delete(post);
        await _postsRepository.SaveChangesAsync();
    }
}