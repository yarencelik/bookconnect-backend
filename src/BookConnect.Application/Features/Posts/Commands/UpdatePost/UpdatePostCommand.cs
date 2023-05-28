using BookConnect.Application.Features.Posts.Models;
using MediatR;
using Microsoft.AspNetCore.JsonPatch;

namespace BookConnect.Application.Features.Posts.Commands.UpdatePost;

public record UpdatePostCommand(string postId, JsonPatchDocument<UpdatePostDto> updatePost) : IRequest;