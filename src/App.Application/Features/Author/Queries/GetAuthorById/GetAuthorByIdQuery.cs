using App.Application.Features.Author.Models;
using MediatR;

namespace App.Application.Features.Author.Queries.GetAuthorById;

public record GetAuthorsByIdQuery(string authorId) : IRequest<AuthorDetailsDto>;