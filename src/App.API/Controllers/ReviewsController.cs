using App.API.Common;
using App.Application.Common.Exceptions;
using App.Application.Features.Reviews.Commands.AddReview;
using App.Application.Features.Reviews.Commands.DeleteReviewById;
using App.Application.Features.Reviews.Commands.UpdateReview;
using App.Application.Features.Reviews.Models;
using App.Application.Features.Reviews.Queries;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("api/v1/[controller]")]
public class ReviewsController : BaseController
{
    public ReviewsController(IMediator mediator) : base(mediator)
    {
    }

    [HttpGet("{reviewId}")]
    public async Task<ActionResult<ReviewDetailsDto>> GetReviewById(string reviewId)
    {
        try
        {
            var request = new GetReviewByIdQuery(reviewId);

            var results = await mediator.Send(request);

            return Ok(results);
        }
        catch(Exception ex)
        {
            if(ex is NotFoundException notFound)
            {
                return NotFound(new
                {
                    errorMessage = ex.Message,
                });
            }
            return StatusCode(StatusCodes.Status500InternalServerError,
            new 
            {
                errorMessage = ex.Message
            });
        }
    }

    [Authorize]
    [HttpPost]
    public async Task<IActionResult> AddBookReview(AddReviewCommand addReview)
    {
        try
        {
            await mediator.Send(addReview);

            return StatusCode(StatusCodes.Status201Created);
        }
        catch(Exception ex)
        {
            if(ex is ValidationException validation)
            {
                return BadRequest(new
                {
                    errors = validation.Errors
                });
            }

            if(ex is NotFoundException notFound)
            {
                return NotFound(new
                {
                    errorMessage = ex.Message,
                });
            }
            return StatusCode(StatusCodes.Status500InternalServerError,
            new 
            {
                errorMessage = ex.Message
            });
        }
    }
    
    [Authorize]
    [HttpPatch("{reviewId}")]
    public async Task<ActionResult> UpdateReview(string reviewId, JsonPatchDocument<UpdateReviewDto> UpdateReview)
    {
        try
        {
            var request = new UpdateReviewCommand(reviewId, UpdateReview);

            await mediator.Send(request);

            return NoContent();
        }
        catch (Exception ex)
        {
            if(ex is NotFoundException notFound)
            {
                return NotFound(new 
                {
                    errorMessage = notFound.Message
                });
            }
            if(ex is ValidationException validation)
            {
                return BadRequest(new 
                {
                    errors = validation.Errors
                });
            }
            return StatusCode(StatusCodes.Status500InternalServerError,
            new 
            {
                errorMessage = ex.Message
            });
        }
    }

    [Authorize]
    [HttpDelete("{reviewId}")]
    public async Task<ActionResult> DeleteReviewById(string reviewId)
    {
        try
        {
            var request = new DeleteReviewByIdCommand(reviewId);

            await mediator.Send(request);

            return NoContent();
        }
        catch (Exception ex)
        {
            if(ex is NotFoundException notFound)
            {
                return NotFound(new 
                {
                    errorMessage = notFound.Message
                });
            }
            return StatusCode(StatusCodes.Status500InternalServerError,
            new 
            {
                errorMessage = ex.Message
            });
        }
    }
}