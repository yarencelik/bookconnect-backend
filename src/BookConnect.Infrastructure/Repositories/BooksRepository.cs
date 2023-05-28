using BookConnect.Application.Features.Books;
using BookConnect.Domain.Entities;
using BookConnect.Infrastructure.Persistence;

namespace BookConnect.Infrastructure.Repositories;

public class BooksRepository : RepositoryBase<Book>, IBooksRepository
{
    public BooksRepository(ApplicationDbContext context) : base(context)
    {
    }
}
