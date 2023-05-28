using App.Application.Features.Posts.Models;
using MediatR;
using Microsoft.AspNetCore.JsonPatch;

namespace App.Application.Features.Posts.Commands.UpdatePost;

public record UpdatePostCommand(string postId, JsonPatchDocument<UpdatePostDto> updatePost) : IRequest;