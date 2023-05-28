using App.Application.Features.Auth.Commands;
using App.Application.Features.Auth.Models;
using App.Domain.Entities;
using AutoMapper;

namespace App.Application.Features.Auth;
public class AuthProfile : Profile
{
    public AuthProfile()
    {
        CreateMap<User, AuthDetailsDto>();
    }
}
