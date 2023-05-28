using AutoMapper;
using BookConnect.Application.Features.Shelves.Commands.CreateShelf;
using BookConnect.Application.Features.Shelves.Models;
using BookConnect.Domain.Entities;

namespace BookConnect.Application.Features.Shelves;

public class ShelfProfile : Profile 
{
    public ShelfProfile()
    {
        CreateMap<Shelf, ShelvesDetailsDto>();
        CreateMap<BookShelf, ShelfDetailsDto>();

        CreateMap<CreateShelfCommand, Shelf>();
        CreateMap<Shelf, UpdateShelfDto>();
        CreateMap<UpdateShelfDto, Shelf>();
    } 
}