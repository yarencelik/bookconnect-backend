using App.Application.Features.Books.Commands.AddBook;
using App.Application.Features.Books.Models;
using App.Domain.Entities;
using AutoMapper;

namespace App.Application.Features.Books;
public class BooksProfile : Profile
{
    public BooksProfile()
    {
        CreateMap<Book, BookDetailsDto>()
            .ForMember(x => x.AuthorName, src => src.MapFrom(x => x.Author!.AuthorName));
        CreateMap<AddBookCommand, Book>();
    }
}
