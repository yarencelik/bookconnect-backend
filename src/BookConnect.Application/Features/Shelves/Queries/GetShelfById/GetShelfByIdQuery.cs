using BookConnect.Application.Common.Models;
using BookConnect.Application.Features.Shelves.Models;

namespace BookConnect.Application.Features.Shelves.Queries.GetShelfById;

// Return BookShelf
public class GetShelfByIdQuery : BaseQuery<ShelfDetailsDto>
{
    public required string ShelfId { get; set; }
}