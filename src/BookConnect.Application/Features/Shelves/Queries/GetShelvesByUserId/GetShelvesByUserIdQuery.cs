using BookConnect.Application.Common.Models;
using BookConnect.Application.Features.Shelves.Models;

namespace BookConnect.Application.Features.Shelves.Queries.GetShelvesByUserId;

// Return List of Shelves
public class GetShelvesByUserIdQuery : BaseQuery<ShelvesDetailsDto>
{
    public required string UserId { get; set; }
    public required string ShelfName { get; set; }  
}