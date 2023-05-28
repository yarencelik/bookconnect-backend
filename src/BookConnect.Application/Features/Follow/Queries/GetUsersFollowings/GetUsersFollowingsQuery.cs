using App.Application.Common.Models;
using App.Application.Features.Users.Models;

namespace App.Application.Features.Follow.Queries.GetUsersFollowings;

public class GetUsersFollowingsQuery : BaseQuery<UserDetailsDto>
{
    public required string UserId { get; set; }   
}