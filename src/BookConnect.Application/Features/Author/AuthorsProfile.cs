using AutoMapper;
using BookConnect.Application.Features.Author.Commands.AddAuthor;
using BookConnect.Application.Features.Author.Models;
using AuthorEntity = BookConnect.Domain.Entities.Author;

namespace BookConnect.Application.Features.Author;

public class AuthorsProfile : Profile
{
    public AuthorsProfile()
    {
        CreateMap<AuthorEntity, AuthorDetailsDto>();
        CreateMap<AddAuthorCommand, AuthorEntity>();
        CreateMap<AuthorEntity, UpdateAuthorDto>();
        CreateMap<UpdateAuthorDto, AuthorEntity>();
    }
}