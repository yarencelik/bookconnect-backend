using App.Application.Features.Auth.Models;
using MediatR;

namespace App.Application.Features.Auth.Commands.LoginUser;

public record LoginUserCommand(string Username, string Password) : IRequest<(AuthDetailsDto, string)>;

