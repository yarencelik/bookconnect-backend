using BookConnect.Application.Common.Interfaces;
using AuthorEntity = BookConnect.Domain.Entities.Author;

namespace BookConnect.Application.Features.Author;

public interface IAuthorsRepository: IRepositoryBase<AuthorEntity>
{

}