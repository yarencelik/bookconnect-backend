using BookConnect.Application.Common.Interfaces;
using BookConnect.Domain.Entities;

namespace BookConnect.Application.Features.Shelves;

public interface IShelfService 
{
    IEnumerable<Shelf> GenerateShelves(Guid userId);
}