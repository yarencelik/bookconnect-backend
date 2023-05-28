using BookConnect.Domain.Enums;
using MediatR;

namespace BookConnect.Application.Features.Users.Commands.CreateUser;

public sealed class CreateUserCommand : IRequest
{
    public required string Username { get; set; }
    public required string Password { get; set; }
    public required string Email { get; set; }
    public string? FirstName { get; set; }
    public string? LastName { get; set; }
    public UserRole Role { get; set; }
}

