using MediatR;

namespace BookConnect.Application.Features.Auth.Commands.LogoutUser;

public record LogoutUserCommand : IRequest;