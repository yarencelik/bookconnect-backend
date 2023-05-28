using BookConnect.Application.Features.Follow.Models;
using MediatR;

namespace BookConnect.Application.Features.Follow.Queries.GetUsersFollowersAndFollowingsCount;

public record GetUsersFollowersAndFollowingsCountQuery(string userId) : IRequest<FollowersAndFollowingsDto>;