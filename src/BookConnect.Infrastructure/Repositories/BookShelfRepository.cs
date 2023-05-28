using BookConnect.Application.Features.Shelves;
using BookConnect.Domain.Entities;
using BookConnect.Infrastructure.Persistence;

namespace BookConnect.Infrastructure.Repositories;

public class BookShelfRepository : RepositoryBase<BookShelf>, IBookShelfRepository
{
    public BookShelfRepository(ApplicationDbContext context) : base(context)
    {
    }
}