using ValidationException = BookConnect.Application.Common.Exceptions.ValidationException;
using AutoMapper;
using FluentValidation;
using MediatR;
using BookConnect.Application.Features.Reviews.Models;
using BookConnect.Application.Common.Exceptions;

namespace BookConnect.Application.Features.Reviews.Commands.UpdateReview;

sealed class UpdateReviewCommandHandler : IRequestHandler<UpdateReviewCommand>
{
    private readonly IReviewsRepository _reviewsRepository;
    private readonly IMapper _mapper;
    private readonly IValidator<UpdateReviewDto> _validator;
    public UpdateReviewCommandHandler(IReviewsRepository reviewsRepository, IMapper mapper, IValidator<UpdateReviewDto> validator)
    {
        _reviewsRepository = reviewsRepository;
        _mapper = mapper;
        _validator = validator;
    }
    public async Task Handle(UpdateReviewCommand request, CancellationToken cancellationToken)
    {
        var review = await _reviewsRepository.GetValue(x => x.Id.ToString() == request.reviewId, null, false)
            ?? throw new NotFoundException($"Review with ID: {request.reviewId} was not found.");

        var reviewToUpdate = _mapper.Map<UpdateReviewDto>(review);
        
        request.UpdateReview.ApplyTo(reviewToUpdate, (e) => 
        {
            throw new ConflictException("Error in JSONPatchDocument " + e.ErrorMessage);
        });

        var validationResults = await _validator.ValidateAsync(reviewToUpdate);
        if(!validationResults.IsValid)
            throw new ValidationException(validationResults.Errors);
        
       _mapper.Map(reviewToUpdate, review);
        await _reviewsRepository.SaveChangesAsync();
    }
}
