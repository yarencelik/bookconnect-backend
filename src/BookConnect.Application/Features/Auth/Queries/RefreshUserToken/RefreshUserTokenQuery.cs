using BookConnect.Application.Features.Auth.Models;
using MediatR;

namespace BookConnect.Application.Features.Auth.Queries.RefreshUserToken;

public record RefreshUserTokenQuery(string OldToken) : IRequest<(AuthDetailsDto, string)>;