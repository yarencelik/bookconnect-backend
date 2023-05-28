using App.Application.Features.Follow.Models;
using MediatR;

namespace App.Application.Features.Follow.Queries.GetUsersFollowersAndFollowingsCount;

public record GetUsersFollowersAndFollowingsCountQuery(string userId) : IRequest<FollowersAndFollowingsDto>;