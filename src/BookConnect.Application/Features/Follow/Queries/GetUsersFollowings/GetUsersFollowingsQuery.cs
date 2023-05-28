using BookConnect.Application.Common.Models;
using BookConnect.Application.Features.Users.Models;

namespace BookConnect.Application.Features.Follow.Queries.GetUsersFollowings;

public class GetUsersFollowingsQuery : BaseQuery<UserDetailsDto>
{
    public required string UserId { get; set; }   
}