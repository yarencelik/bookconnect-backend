using App.Application.Common.Exceptions;
using App.Application.Features.Posts.Models;
using ValidationException = App.Application.Common.Exceptions.ValidationException;
using AutoMapper;
using FluentValidation;
using MediatR;

namespace App.Application.Features.Posts.Commands.UpdatePost;

sealed class UpdatePostCommandHandler : IRequestHandler<UpdatePostCommand>
{
    private readonly IPostsRepository _postsRepository;
    private readonly IMapper _mapper;
    private readonly IValidator<UpdatePostDto> _validator;
    public UpdatePostCommandHandler(IPostsRepository postsRepository, IValidator<UpdatePostDto> validator, IMapper mapper)
    {
       _postsRepository = postsRepository;
       _validator = validator;
       _mapper = mapper;        
    }
    public async Task Handle(UpdatePostCommand request, CancellationToken cancellationToken)
    {
        var post = await _postsRepository.GetValue(x => x.Id.ToString() == request.postId)
            ?? throw new NotFoundException($"Post with Id '{request.postId}' was not found.");

        var postToUpdate = _mapper.Map<UpdatePostDto>(post);

        request.updatePost.ApplyTo(postToUpdate, x => throw new Exception("Error in JsonPatchDocument"));

        var validationResult = await _validator.ValidateAsync(postToUpdate);

        if(!validationResult.IsValid)
        {
            throw new ValidationException(validationResult.Errors);
        }

        var updatedPost = _mapper.Map(postToUpdate, post);

        await _postsRepository.SaveChangesAsync();
    }
}