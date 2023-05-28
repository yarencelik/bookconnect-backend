using MediatR;

namespace App.Application.Features.Auth.Commands.LogoutUser;

public record LogoutUserCommand : IRequest;