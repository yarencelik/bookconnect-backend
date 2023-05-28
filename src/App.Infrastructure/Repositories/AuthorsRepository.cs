using App.Application.Features.Author;
using App.Domain.Entities;
using App.Infrastructure.Persistence;

namespace App.Infrastructure.Repositories;

public class AuthorsRepository : RepositoryBase<Author>, IAuthorsRepository
{
    public AuthorsRepository(ApplicationDbContext context) : base(context)
    {
    }
}