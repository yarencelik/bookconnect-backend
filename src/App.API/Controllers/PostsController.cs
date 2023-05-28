using App.API.Common;
using App.Application.Common.Exceptions;
using App.Application.Common.Models;
using App.Application.Features.Likes.Commands.AddLike;
using App.Application.Features.Likes.Commands.RemoveLike;
using App.Application.Features.Posts.Commands.CreatePost;
using App.Application.Features.Posts.Commands.DeletePost;
using App.Application.Features.Posts.Commands.UpdatePost;
using App.Application.Features.Posts.Models;
using App.Application.Features.Posts.Queries;
using App.Application.Features.Posts.Queries.GetPosts;
using App.Application.Features.Posts.Queries.GetPostsByFollowers;
using App.Application.Features.Posts.Queries.GetPostsById;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;

namespace App.API.Controllers;

[ApiController]
[Route("api/v1/[controller]")]
public sealed class PostsController : BaseController
{
    public PostsController(IMediator mediator): base(mediator)
    {
    }

    [HttpGet]
    public async Task<ActionResult<PaginatedResults<PostsDetailsDto>>> GetPosts(string? search, int page = 1, int pageSize = 10)
    {
        try
        {
            var request = new GetPostsQuery
            {
                // Search = search,
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

    [Authorize]
    [HttpGet("followersPosts")]
    public async Task<ActionResult> GetPostsByFollowers()
    {
        try
        {
            var request = new GetPostsByFollowersQuery();

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
    [HttpGet("{postId}")]
    public async Task<ActionResult> GetPostById(string postId)
    {
        try
        {
            var request = new GetPostByIdQuery(postId);
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
    [HttpPost]
    public async Task<IActionResult> CreatePost(CreatePostCommand createPost)
    {
        try
        {
            var result = await mediator.Send(createPost);

            return CreatedAtAction("GetPostById", new {
                postId = result
            }, result);

        }
        catch (Exception ex)
        {
            if(ex is ValidationException validation) 
            {
                return BadRequest(new 
                {
                    errorMessage = validation.Message
                });
            }
            return StatusCode(StatusCodes.Status500InternalServerError,
            new
            {
                errorMessage = ex.Message
            });
        }
    }

    [HttpPatch("{postId}")]
    public async Task<IActionResult> UpdatePost(string postId, JsonPatchDocument<UpdatePostDto> updatePost)
    {
        try
        {
            var request = new UpdatePostCommand(postId, updatePost);
            await mediator.Send(request);

            return NoContent();
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

    [HttpDelete("{postId}")]
    public async Task<IActionResult> DeletePost(string postId)
    {
        try
        {
            var request = new DeletePostCommand(postId);
            await mediator.Send(request);

            return NoContent();
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
    [HttpPost("{postId}/like")]
    public async Task<IActionResult> AddLikes(string postId)
    {
        try
        {
            var request = new AddLikeCommand(postId);
            await mediator.Send(request);

            return Accepted();
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
    [HttpPost("{postId}/unlike")]
    public async Task<IActionResult> RemoveLikes(string postId)
    {
        try
        {
            var request = new RemoveLikeCommand(postId);
            await mediator.Send(request);

            return Accepted();
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
}