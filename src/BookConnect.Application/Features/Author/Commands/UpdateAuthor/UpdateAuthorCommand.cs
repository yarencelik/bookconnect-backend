using App.Application.Features.Author.Models;
using MediatR;
using Microsoft.AspNetCore.JsonPatch;

namespace App.Application.Features.Author.Commands.UpdateAuthor;

public record UpdateAuthorCommand(string authorId, JsonPatchDocument<UpdateAuthorDto> UpdateAuthorDto): IRequest;