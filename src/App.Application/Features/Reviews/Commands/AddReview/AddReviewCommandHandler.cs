using App.Application.Common.Exceptions;
using App.Application.Common.Interfaces;
using App.Application.Features.Books;
using App.Application.Features.Posts;
using App.Application.Features.Users;
using App.Domain.Entities;
using AutoMapper;
using MediatR;

namespace App.Application.Features.Reviews.Commands.AddReview;

sealed class AddReviewCommandHandler: IRequestHandler<AddReviewCommand>
{
    private readonly IReviewsRepository _reviewsRepository;
    private readonly ICurrentUserService _currentUserService;
    private readonly IUsersRepository _usersRepository;
    private readonly IBooksRepository _booksRepository;
    private readonly IPostsRepository _postsRepository;
    private readonly IMapper _mapper;
    public AddReviewCommandHandler(IReviewsRepository reviewsRepository, IUsersRepository usersRepository, IBooksRepository booksRepository, IMapper mapper, IPostsRepository postsRepository, ICurrentUserService currentUserService)
    {
        _reviewsRepository = reviewsRepository;
        _currentUserService = currentUserService;
        _usersRepository = usersRepository;
        _booksRepository = booksRepository;
        _postsRepository = postsRepository;
        _mapper = mapper;
    }

    public async Task Handle(AddReviewCommand request, CancellationToken cancellationToken)
    {
        // Check if user already made a review on the requested book
        var existingReview = await _reviewsRepository.GetValue(
            x => x.Reviewer_Id.ToString() == _currentUserService.UserId &&
            x.Book_Id.ToString() == request.Book_Id
            );

        if (existingReview != null)
            throw new ConflictException("User already made a review on this Book");
            
        var user = await _usersRepository.GetValue(x => x.Id.ToString() == _currentUserService.UserId, null, false)
            ?? throw new NotFoundException("User was not found");
        var book = await _booksRepository.GetValue(x => x.Id.ToString() == request.Book_Id, null, false)
            ?? throw new NotFoundException($"Book with the ID '{request.Book_Id}' was not found.");
        
        var mappedReview = _mapper.Map<Review>(request);
        mappedReview.Reviewer_Id = Guid.Parse(_currentUserService.UserId ?? "");

        await _reviewsRepository.Create(mappedReview);
        await _reviewsRepository.SaveChangesAsync();

        Post newPost = new()
        {
            Title = "Book Review",
            Body = mappedReview.Description ?? string.Empty,
            Owner = user,
            Review_Post_Id = mappedReview.Id
        };

        await _postsRepository.Create(newPost);
        await _postsRepository.SaveChangesAsync();
    }
}