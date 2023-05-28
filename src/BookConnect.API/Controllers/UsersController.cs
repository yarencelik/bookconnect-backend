using App.API.Common;
using App.Application.Common.Exceptions;
using App.Application.Common.Models;
using App.Application.Features.Follow.Commands.FollowUser;
using App.Application.Features.Follow.Commands.UnfollowUser;
using App.Application.Features.Follow.Models;
using App.Application.Features.Follow.Queries.GetUsersFollowers;
using App.Application.Features.Follow.Queries.GetUsersFollowersAndFollowingsCount;
using App.Application.Features.Follow.Queries.GetUsersFollowings;
using App.Application.Features.Posts.Models;
using App.Application.Features.Posts.Queries;
using App.Application.Features.Reviews.Models;
using App.Application.Features.Reviews.Queries.GetUsersReviews;
using App.Application.Features.Users.Models;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("api/v1/[controller]")]
public class UsersController : BaseController
{
    public UsersController(IMediator mediator) : base(mediator)
    {
    }

    [HttpGet("{userId}/posts")]
    public async Task<ActionResult<PaginatedResults<PostsDetailsDto>>> GetPostsByOwnerId(string userId, int page = 1, int pageSize = 10)
    {
        try
        {
            var request = new GetPostsByOwnerIdQuery
            {
                OwnerId = userId,
                Page = page,
                PageSize = pageSize
            };

            var results = await mediator.Send(request);
            return Ok(results);
        }
        catch(Exception ex)
        {
            return StatusCode(StatusCodes.Status500InternalServerError,
            new
            {
                errorMessage = ex.Message
            });
        }
    }
    [HttpGet("{userId}/followers")]
    public async Task<ActionResult<PaginatedResults<UserDetailsDto>>> GetUsersFollowers(string userId, int page = 1, int pageSize = 10)
    {
       try
       {
            var request = new GetUsersFollowersQuery
            {
                UserId = userId,
                Page = page,
                PageSize = pageSize
            };

            var results = await mediator.Send(request);

            return Ok(results);
       }
       catch(Exception ex) 
       {
            return StatusCode(StatusCodes.Status500InternalServerError, 
            new 
            {
                errorMesage = ex.Message
            });
       }
    }

    [HttpGet("{userId}/followings")]
    public async Task<ActionResult<PaginatedResults<UserDetailsDto>>> GetUsersFollowings(string userId, int page = 1, int pageSize = 10)
    {
       try
       {
            var request = new GetUsersFollowingsQuery
            {
                UserId = userId,
                Page = page,
                PageSize = pageSize
            };

            var results = await mediator.Send(request);

            return Ok(results);
       }
       catch(Exception ex) 
       {
            return StatusCode(StatusCodes.Status500InternalServerError, 
            new 
            {
                errorMesage = ex.Message
            });
       }
    }

    [HttpGet("{userId}/ffcount")]
    public async Task<ActionResult<FollowersAndFollowingsDto>> GetUsersFollowersAndFollowingsCount(string userId)
    {
        try
        {
            var request = new GetUsersFollowersAndFollowingsCountQuery(userId);

            var results = await mediator.Send(request);

            return Ok(results);
        }
        catch(Exception ex)
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

    [Authorize]
    [HttpPost("{userId}/follow")]
    public async Task<ActionResult> FollowUser(string userId)
    {
        try
        {
            var request = new FollowUserCommand(userId);
            await mediator.Send(request);

            return Ok();
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
            if(ex is ConflictException conflict)
            {
                return BadRequest(new 
                {
                    errorMessage = conflict.Message
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
    [HttpPost("{userId}/unfollow")]
    public async Task<ActionResult> UnfollowUser(string userId)
    {
        try
        {
            var request = new UnfollowUserCommand(userId);
            await mediator.Send(request);

            return Ok();
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
            if(ex is ConflictException conflict)
            {
                return BadRequest(new 
                {
                    errorMessage = conflict.Message
                });
            }
            return StatusCode(StatusCodes.Status500InternalServerError,
            new
            {
                errorMessage = ex.Message
            });
        }
    }

    [HttpGet("{userId}/reviews")]
    public async Task<ActionResult<PaginatedResults<ReviewDetailsDto>>> GetBookReviews(
        string userId, 
        int page = 1, 
        int pageSize = 10, 
        bool includeReviewer = false, 
        bool includeBook = false)
    {
        try
        {
            var request = new GetUsersReviewsQuery 
            {
                UserId = userId,
                Page = page,
                PageSize = pageSize,
                IncludeBook = includeBook,
                IncludeReviewer = includeReviewer
            };

            var results = await mediator.Send(request);

            return Ok(results);
        }
        catch (Exception ex)
        {
            
            return StatusCode(StatusCodes.Status500InternalServerError,
            new 
            {
                errorMessage = ex.Message
            });
        }
    }
}