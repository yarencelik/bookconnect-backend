using App.Application.Common.Models;
using App.Application.Features.Users.Models;

namespace App.Application.Features.Follow.Queries.GetUsersFollowers;

public class GetUsersFollowersQuery : BaseQuery<UserDetailsDto>
{
    public required string UserId { get; set; }
}