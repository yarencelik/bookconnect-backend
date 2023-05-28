using AutoMapper;
using BookConnect.Application.Features.Users.Commands.CreateUser;
using BookConnect.Application.Features.Users.Models;
using BookConnect.Domain.Entities;

namespace BookConnect.Application.Features.Users;

public class UsersProfile : Profile
{
   public UsersProfile()
   {
        CreateMap<User, UserDetailsDto>();
        CreateMap<CreateUserDto, CreateUserCommand>();
        CreateMap<CreateUserCommand, User>();
   } 
}