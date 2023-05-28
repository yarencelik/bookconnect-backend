using BookConnect.Application.Features.Shelves;
using BookConnect.Domain.Entities;
using BookConnect.Infrastructure.Persistence;

namespace BookConnect.Infrastructure.Repositories;

public class ShelfRepository : RepositoryBase<Shelf>, IShelfRepository
{
    public ShelfRepository(ApplicationDbContext context) : base(context)
    {
    }
}