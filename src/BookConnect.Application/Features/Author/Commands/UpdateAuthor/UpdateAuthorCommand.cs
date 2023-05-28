using BookConnect.Application.Features.Author.Models;
using MediatR;
using Microsoft.AspNetCore.JsonPatch;

namespace BookConnect.Application.Features.Author.Commands.UpdateAuthor;

public record UpdateAuthorCommand(string authorId, JsonPatchDocument<UpdateAuthorDto> UpdateAuthorDto): IRequest;