using App.Application.Features.Users.Commands;
using App.Application.Features.Users.Models;
using App.Domain.Entities;
using AutoMapper;

namespace App.Application.Features.Users;

public class UsersProfile : Profile
{
   public UsersProfile()
   {
        CreateMap<User, UserDetailsDto>();
        CreateMap<CreateUserCommand, User>();
   } 
}