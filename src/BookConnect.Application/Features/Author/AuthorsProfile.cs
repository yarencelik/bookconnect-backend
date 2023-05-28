using App.Application.Features.Author.Commands.AddAuthor;
using App.Application.Features.Author.Models;
using App.Domain.Entities;
using AutoMapper;

public class AuthorsProfile : Profile
{
    public AuthorsProfile()
    {
        CreateMap<Author, AuthorDetailsDto>();
        CreateMap<AddAuthorCommand, Author>();
        CreateMap<Author, UpdateAuthorDto>();
        CreateMap<UpdateAuthorDto, Author>();
    }
}