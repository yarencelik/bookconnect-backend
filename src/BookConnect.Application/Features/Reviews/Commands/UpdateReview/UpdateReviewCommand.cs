using BookConnect.Application.Features.Reviews.Models;
using MediatR;
using Microsoft.AspNetCore.JsonPatch;

namespace BookConnect.Application.Features.Reviews.Commands.UpdateReview;

public record UpdateReviewCommand (string reviewId, JsonPatchDocument<UpdateReviewDto> UpdateReview) : IRequest;