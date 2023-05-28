using BookConnect.Application.Common.Interfaces;
using BookConnect.Domain.Entities;

namespace BookConnect.Application.Features.Books;
public interface IBooksRepository : IRepositoryBase<Book>
{
}
