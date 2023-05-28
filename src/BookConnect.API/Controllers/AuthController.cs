using AutoMapper;
using BookConnect.API.Common;
using BookConnect.Application.Common.Exceptions;
using BookConnect.Application.Common.Interfaces;
using BookConnect.Application.Features.Auth.Commands.LoginUser;
using BookConnect.Application.Features.Auth.Commands.LogoutUser;
using BookConnect.Application.Features.Auth.Models;
using BookConnect.Application.Features.Auth.Queries.RefreshUserToken;
using BookConnect.Application.Features.Users.Commands;
using BookConnect.Application.Features.Users.Commands.CreateUser;
using BookConnect.Application.Features.Users.Models;
using BookConnect.Application.Features.Users.Queries;
using BookConnect.Domain.Enums;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BookConnect.API.Controllers;

[ApiController]
[Route("api/v1/[controller]")]
public class AuthController : BaseController
{
    private readonly ICurrentUserService _currentUserService;
    private readonly IMapper _mapper;
    public AuthController(IMediator mediator, IMapper mapper, ICurrentUserService currentUserService): base(mediator)
    {
        _currentUserService = currentUserService;
        _mapper = mapper;
    }

    [HttpPost("login")]
    public async Task<ActionResult<AuthDetailsDto>> LoginUser(LoginUserCommand loginUser)
    {
        try
        {
            var (result, refreshToken) = await mediator.Send(loginUser);

            Response.Cookies.Append("rt", refreshToken, new CookieOptions()
            {
                MaxAge = TimeSpan.FromDays(7),
                HttpOnly = true
            });

            return Ok(result);
        }
        catch (Exception ex)
        {
            if (ex is ValidationException validation)
            {
                return BadRequest(new
                {
                    errors = validation.Errors,
                });
            }
            return BadRequest(new
            {
                errorMessage = ex.Message
            });
        }
    }

    [HttpPost("register")]
    public async Task<ActionResult> RegisterUser(CreateUserDto createUser)
    {
        try
        {
            var mappedRequest = _mapper.Map<CreateUserCommand>(createUser);
            mappedRequest.Role = UserRole.Reader;

            await mediator.Send(mappedRequest);
            return Ok();
        }
        catch (Exception ex)
        {
            if (ex is ValidationException validation)
            {
                return BadRequest(new
                {
                    errors = validation.Errors
                });
            }
            return BadRequest(new
            {
                errorMessage = ex.Message
            });
        }
    }

    [HttpGet("refresh")]
    public async Task<ActionResult<AuthDetailsDto>> RefreshUserToken()
    {
        try
        {
            var oldToken = Request.Cookies["rt"];
            if (string.IsNullOrEmpty(oldToken))
            {
                return NotFound(new
                {
                    errorMessage = "Refresh Token was not found."
                });
            }

            var request = new RefreshUserTokenQuery(oldToken);
            var (result, refreshToken) = await mediator.Send(request);

            Response.Cookies.Append("rt", refreshToken, new CookieOptions()
            {
                MaxAge = TimeSpan.FromDays(7),
                HttpOnly = true
            });

            return Ok(result);
        }
        catch (Exception ex)
        {
            return BadRequest(new
            {
                errorMessage = ex.Message
            });
        }
    }

    [Authorize]
    [HttpGet("user")]
    public async Task<ActionResult<UserDetailsDto>> GetCurrentUser()
    {
        try
        {
            var userId = _currentUserService.UserId;

            var request = new GetUserByIdQuery(userId);

            var result = await mediator.Send(request);

            return result;
        }
        catch(Exception ex)
        {
            return BadRequest(new 
            {
                errorMessage = ex.Message
            });
        }
    }

    [Authorize]
    [HttpPost("logout")]
    public async Task<IActionResult> LogoutUser()
    {
        try
        {
            var request = new LogoutUserCommand();
            await mediator.Send(request);

            Response.Cookies.Append("rt", "", new CookieOptions()
            {
                MaxAge = TimeSpan.Zero,
                HttpOnly = true
            });

            return NoContent();
        }
        catch (Exception ex)
        {
            return BadRequest(new
            {
                errorMessage = ex.Message
            });
        }
    }
}