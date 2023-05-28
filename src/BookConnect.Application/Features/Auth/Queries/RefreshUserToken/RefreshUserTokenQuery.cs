using App.Application.Features.Auth.Models;
using MediatR;

namespace App.Application.Features.Auth.Queries.RefreshUserToken;

public record RefreshUserTokenQuery(string OldToken) : IRequest<(AuthDetailsDto, string)>;