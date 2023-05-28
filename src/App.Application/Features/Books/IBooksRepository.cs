using App.Application.Common.Interfaces;
using App.Application.Common.Models;
using App.Domain.Entities;

namespace App.Application.Features.Books;
public interface IBooksRepository : IRepositoryBase<Book>
{
}
