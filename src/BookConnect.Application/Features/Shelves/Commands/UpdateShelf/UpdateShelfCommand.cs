using BookConnect.Application.Features.Shelves.Models;
using MediatR;
using Microsoft.AspNetCore.JsonPatch;

namespace BookConnect.Application.Features.Shelves.Commands.UpdateShelf;

public record UpdateShelfCommand(string shelfId, JsonPatchDocument<UpdateShelfDto> UpdateShelf) : IRequest;