using BookConnect.API.Common;
using BookConnect.Application.Common.Exceptions;
using BookConnect.Application.Common.Models;
using BookConnect.Application.Features.Shelves.Commands.AddBooksToShelf;
using BookConnect.Application.Features.Shelves.Commands.CreateShelf;
using BookConnect.Application.Features.Shelves.Commands.DeleteShelfById;
using BookConnect.Application.Features.Shelves.Commands.RemoveBooksToShelf;
using BookConnect.Application.Features.Shelves.Commands.UpdateShelf;
using BookConnect.Application.Features.Shelves.Models;
using BookConnect.Application.Features.Shelves.Queries.GetShelfById;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;

namespace BookConnect.API.Controllers;

[ApiController]
[Route("api/v1/[controller]")]
public class ShelfController: BaseController
{
    public ShelfController(IMediator mediator) : base(mediator)
    {
    }

    [HttpGet("{shelfId}", Name = "GetShelfById")]
    public async Task<ActionResult<PaginatedResults<ShelfDetailsDto>>> GetShelfById(string shelfId,int page = 1, int pageSize = 10)
    {
        try
        {
            var request = new GetShelfByIdQuery
            {
                ShelfId = shelfId,
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
    [HttpPost]
    public async Task<ActionResult> CreateShelf(CreateShelfCommand createShelf)
    {
        try
        {
            var results = await mediator.Send(createShelf);

            return CreatedAtRoute("GetShelfById", new {shelfId = results}, results);
        }
        catch(Exception ex)
        {
            if(ex is UnauthorizedAccessException unauthorized)
            {
                return Unauthorized(new {
                    errorMessage = unauthorized.Message
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
    [HttpPatch("{shelfId}")]
    public async Task<ActionResult> UpadteShelf(string shelfId, JsonPatchDocument<UpdateShelfDto> UpdateShelf)
    {
        try
        {
            var request = new UpdateShelfCommand(shelfId, UpdateShelf);
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
            if(ex is ConflictException conflict)
            {
                return BadRequest(new 
                {
                    errorMessage = ex.Message
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
    [HttpDelete("{shelfId}")]
    public async Task<ActionResult> DeleteShelfById(string shelfId)
    {
        try
        {
            var request = new DeleteShelfByIdCommand(shelfId);
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
    [HttpPost("{shelfId}/addbooks")]
    public async Task<ActionResult> AddBooksToShelf(string shelfId, AddBooksToShelfDto addBooks)
    {
        try
        {
            var request = new AddBooksToShelfCommand(shelfId, addBooks.BookId);
            var results = await mediator.Send(request);

            return Ok(); 
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
                    errorMessage = ex.Message
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
    [HttpDelete("{shelfId}/removebooks")]
    public async Task<ActionResult> RemoveBooksToShelf(string shelfId, RemoveBooksToShelfDto removeBooks)
    {
        try
        {
            var request = new RemoveBooksToShelfCommand(shelfId, removeBooks.BookId);
            await mediator.Send(request);

            return Ok(); 
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