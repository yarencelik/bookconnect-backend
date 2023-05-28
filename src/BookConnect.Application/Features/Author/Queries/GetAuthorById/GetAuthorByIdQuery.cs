using BookConnect.Application.Features.Author.Models;
using MediatR;

namespace BookConnect.Application.Features.Author.Queries.GetAuthorById;

public record GetAuthorsByIdQuery(string authorId) : IRequest<AuthorDetailsDto>;