using App.API.Common;
using App.Application.Common.Exceptions;
using App.Application.Common.Models;
using App.Application.Features.Author.Commands.AddAuthor;
using App.Application.Features.Author.Commands.DeleteAuthorById;
using App.Application.Features.Author.Commands.UpdateAuthor;
using App.Application.Features.Author.Models;
using App.Application.Features.Author.Queries.GetAuthorById;
using App.Application.Features.Author.Queries.GetAuthors;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;

namespace App.API.Controllers;

[ApiController]
[Route("api/v1/[controller]")]
public class AuthorsController : BaseController
{
    public AuthorsController(IMediator mediator): base(mediator)
    {
    }

    [HttpGet]
    public async Task<ActionResult<PaginatedResults<AuthorDetailsDto>>> GetAuthors(string? name, int page = 1, int pageSize = 10)
    {
        try
        {
            var request = new GetAuthorsQuery
            {
                Name = name ?? string.Empty,
                Page = page,
                PageSize = pageSize
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

    [HttpGet("{authorId}", Name = "GetAuthorByAuthorId")]
    public async Task<ActionResult<AuthorDetailsDto>> GetAuthorById(string authorId)
    {
        try
        {
            var request = new GetAuthorsByIdQuery(authorId);

            var results = await mediator.Send(request);

            return Ok(results);
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

    [Authorize(Roles = "Admin")]
    [HttpPost]
    public async Task<ActionResult> AddAuthor(AddAuthorCommand addAuthors)
    {
        try
        {
            var results = await mediator.Send(addAuthors);

            return CreatedAtRoute("GetAuthorByAuthorId", new {authorId = results}, results);
        }
        catch (Exception ex)
        {
            if(ex is ValidationException validation)
            {
                return BadRequest(new
                {
                    errors = validation.Errors
                });
            }
            if(ex is ConflictException conflict)
            {
                return BadRequest(new 
                {
                    message = conflict.Message
                });
            }
            return StatusCode(StatusCodes.Status500InternalServerError, 
            new
            {
                errorMessage = ex.Message
            });
        }
    }

    [Authorize(Roles = "Admin, Author")]
    [HttpPatch("{authorId}")]
     public async Task<ActionResult> UpdateAuthor(string authorId, JsonPatchDocument<UpdateAuthorDto> updateAuthor)
    {
        try
        {
            var request = new UpdateAuthorCommand(authorId, updateAuthor);
            await mediator.Send(request);

            return NoContent();
        }
        catch (Exception ex)
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
                     errorMessage = notFound.Message
                });
            }
            if(ex is ConflictException conflict)
            {
                return BadRequest(new 
                {
                    message = conflict.Message
                });
            }
            return StatusCode(StatusCodes.Status500InternalServerError,
            new
            {
                errorMessage = ex.Message
            });
        }
    }

    [Authorize(Roles = "Admin")]
    [HttpDelete("{authorId}")]
    public async Task<IActionResult> DeleteAuthor(string authorId)
    {
        try
        {
            var request = new DeleteAuthorByIdCommand(authorId);
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