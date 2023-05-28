using App.Application.Common.Interfaces;
using AuthorEntity = App.Domain.Entities.Author;

namespace App.Application.Features.Author;

public interface IAuthorsRepository: IRepositoryBase<AuthorEntity>
{

}