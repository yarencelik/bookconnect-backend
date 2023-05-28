using App.Application.Features.Posts.Models;
using MediatR;

namespace App.Application.Features.Posts.Queries.GetPostsById;

public record GetPostByIdQuery(string postId) : IRequest<PostsDetailsDto>;