using App.API.Common;
using App.Application.Common.Exceptions;
using App.Application.Common.Interfaces;
using App.Application.Features.Auth.Commands.LoginUser;
using App.Application.Features.Auth.Commands.LogoutUser;
using App.Application.Features.Auth.Models;
using App.Application.Features.Auth.Queries.RefreshUserToken;
using App.Application.Features.Users.Commands;
using App.Application.Features.Users.Models;
using App.Application.Features.Users.Queries;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace App.API.Controllers; 

[ApiController]
[Route("api/v1/[controller]")]
public class AuthController : BaseController
{
    private readonly ICurrentUserService _currentUserService;
    public AuthController(IMediator mediator, ICurrentUserService currentUserService): base(mediator)
    {
        _currentUserService = currentUserService;
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
    public async Task<ActionResult> RegisterUser(CreateUserCommand registerUser)
    {
        try
        {
            await mediator.Send(registerUser);
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