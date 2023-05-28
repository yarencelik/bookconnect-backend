using AutoMapper;
using BookConnect.Application.Features.Posts.Commands.CreatePost;
using BookConnect.Application.Features.Posts.Models;
using BookConnect.Domain.Entities;

namespace BookConnect.Application.Features.Posts;

public class PostsProfile : Profile
{
    public PostsProfile()
    {
        CreateMap<Post, PostsDetailsDto>()
            .ForMember(dest => dest.CreatedBy, src => src.MapFrom(x => x.Owner.Username));
        CreateMap<CreatePostCommand, Post>();
        CreateMap<UpdatePostDto, Post>();
        CreateMap<Post, UpdatePostDto>();
    }   
}