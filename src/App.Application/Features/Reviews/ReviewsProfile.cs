using App.Application.Features.Reviews.Commands.AddReview;
using App.Application.Features.Reviews.Models;
using App.Domain.Entities;
using AutoMapper;

namespace App.Application.Features.Reviews;
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
