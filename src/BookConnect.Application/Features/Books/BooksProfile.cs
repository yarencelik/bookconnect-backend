using AutoMapper;
using BookConnect.Application.Features.Books.Commands.AddBook;
using BookConnect.Application.Features.Books.Models;
using BookConnect.Domain.Entities;

namespace BookConnect.Application.Features.Books;

public class BooksProfile : Profile
{
    public BooksProfile()
    {
        CreateMap<Book, BookDetailsDto>()
            .ForMember(x => x.AuthorName, src => src.MapFrom(x => x.Author!.AuthorName));
        CreateMap<AddBookCommand, Book>();
    }
}
