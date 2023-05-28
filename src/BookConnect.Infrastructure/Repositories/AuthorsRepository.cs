using BookConnect.Application.Features.Author;
using BookConnect.Domain.Entities;
using BookConnect.Infrastructure.Persistence;

namespace BookConnect.Infrastructure.Repositories;

public class AuthorsRepository : RepositoryBase<Author>, IAuthorsRepository
{
    public AuthorsRepository(ApplicationDbContext context) : base(context)
    {
    }
}