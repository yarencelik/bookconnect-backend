using BookConnect.Application.Features.Posts.Models;
using MediatR;

namespace BookConnect.Application.Features.Posts.Queries.GetPostsById;

public record GetPostByIdQuery(string postId) : IRequest<PostsDetailsDto>;