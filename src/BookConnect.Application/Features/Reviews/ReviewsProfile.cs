using AutoMapper;
using BookConnect.Application.Features.Reviews.Commands.AddReview;
using BookConnect.Application.Features.Reviews.Models;
using BookConnect.Domain.Entities;

namespace BookConnect.Application.Features.Reviews;
public sealed class ReviewsProfile : Profile
{
    public ReviewsProfile()
    {
        CreateMap<AddReviewCommand, Review>();
        CreateMap<Review, ReviewDetailsDto>();
        CreateMap<Review, UpdateReviewDto>();
        CreateMap<UpdateReviewDto, Review>();
    }
}
