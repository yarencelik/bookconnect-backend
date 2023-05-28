using AutoMapper;
using BookConnect.Application.Features.Auth.Models;
using BookConnect.Domain.Entities;

namespace BookConnect.Application.Features.Auth;

public class AuthProfile : Profile
{
    public AuthProfile()
    {
        CreateMap<User, AuthDetailsDto>();
    }
}
