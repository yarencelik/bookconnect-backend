using BookConnect.Application.Features.Auth.Models;
using MediatR;

namespace BookConnect.Application.Features.Auth.Commands.LoginUser;

public record LoginUserCommand(string Username, string Password) : IRequest<(AuthDetailsDto, string)>;

