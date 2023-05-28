using App.Application.Features.Posts.Commands.CreatePost;
using App.Application.Features.Posts.Models;
using App.Domain.Entities;
using AutoMapper;

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