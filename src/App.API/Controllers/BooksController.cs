using App.API.Common;
using App.Application.Common.Exceptions;
using App.Application.Common.Models;
using App.Application.Features.Books.Commands.AddBook;
using App.Application.Features.Books.Commands.DeleteBookById;
using App.Application.Features.Books.Models;
using App.Application.Features.Books.Queries.GetBooksById;
using App.Application.Features.Reviews.Models;
using App.Application.Features.Reviews.Queries.GetBooksReviews;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace App.API.Controllers;

[ApiController]
[Route("api/v1/[controller]")]
public class BooksController : BaseController
{

    public BooksController(IMediator mediator): base(mediator)
    {

    }

    [HttpGet]
    public async Task<ActionResult<PaginatedResults<BookDetailsDto>>> GetBooks(string? search, int page = 1, int pageSize = 10)
    {
        try
        {
            var request = new GetBooksQuery
            {
                Search = search ?? string.Empty,
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

    [HttpGet("{booksId}", Name = "GetBooksByBookId")]
    public async Task<ActionResult> GetBooksById(string booksId)


    {
        try
        {
            var request = new GetBooksByIdQuery(booksId);
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


    [Authorize(Roles = "Admin, Author")]
    [HttpPost]
    public async Task<ActionResult> AddBooks(AddBookCommand addBook)
    {
        try
        {
            var id = await mediator.Send(addBook);

            return CreatedAtRoute("GetBooksByBookId", new {booksId = id}, id);
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

    [Authorize(Roles = "Admin, Author")]
    [HttpDelete("{bookId}")]
    public async Task<IActionResult> DeleteBooks(string bookId)
    {
        try
        {
            var request = new DeleteBookCommand(bookId);

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

    [HttpGet("{bookId}/reviews")]
    public async Task<ActionResult<PaginatedResults<ReviewDetailsDto>>> GetBookReviews(
        string bookId, 
        int page = 1, 
        int pageSize = 10, 
        bool includeReviewer = false, 
        bool includeBook = false)
    {
        try
        {
            var request = new GetBookReviewsQuery
            {
                BookId = bookId,
                Page = page,
                PageSize = pageSize,
                IncludeReviewer = includeReviewer,
                IncludeBook = includeBook
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
