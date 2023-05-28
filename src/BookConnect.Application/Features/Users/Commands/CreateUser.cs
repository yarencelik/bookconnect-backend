using App.Domain.Enums;
using System.Linq.Expressions;
using AutoMapper;
using MediatR;
using App.Domain.Entities;
using FluentValidation;
using App.Application.Features.Auth;

// TODO: Improvement:  Register as Author Directly
// Which can be "Verified" by admin if the Author is legitimate
// and also verify if the Author has a linked account.

namespace App.Application.Features.Users.Commands;

public sealed class CreateUserCommand : IRequest
{
    public required string Username { get; set; }
    public required string Password { get; set; }
    public required string Email { get; set; }
    public string? FirstName { get; set; }
    public string? LastName { get; set; }

    public UserRole Role { get; set; }
}

public class CreateUserCommandValidator : AbstractValidator<CreateUserCommand> 
{
    public CreateUserCommandValidator ()
    {
        
        RuleFor(x => x.Username)
            .MinimumLength(4)
            .MaximumLength(12)
            .NotNull()
            .NotEmpty();

        RuleFor(x => x.Password)
            .MinimumLength(4)
            .MaximumLength(12)
            .NotNull()
            .NotEmpty();

        RuleFor(x => x.Email)
            .EmailAddress()
            .NotEmpty()
            .NotNull();
        
        RuleFor(x => x.FirstName)
            .MinimumLength(4)
            .MaximumLength(20);

        
        RuleFor(x => x.LastName)
            .MinimumLength(4)
            .MaximumLength(20);

        RuleFor(x => x.Role)
            .IsInEnum().WithMessage("Invalid User Role")
            .NotEmpty()
            .NotNull();
    }
}

sealed class CreateUserCommandHandler : IRequestHandler<CreateUserCommand>
{
    private readonly IUsersRepository _usersRepository;
    private readonly IPasswordService _passwordService;
    private readonly IMapper _mapper;
    public CreateUserCommandHandler(IUsersRepository usersRepository, IMapper mapper, IPasswordService passwordService)
    {
       _usersRepository = usersRepository;
       _mapper = mapper; 
       _passwordService = passwordService;
    }
    public async Task Handle(CreateUserCommand request, CancellationToken cancellationToken)
    {
        // check if username already exist
        var user = await _usersRepository.GetValue(
            x => x.Username == request.Username ||
            x.Email == request.Email,
            new List<Expression<Func<User, object>>>
            {
                x => x.Author!,
            }
        );
        if(user != null)
        {
            throw new Exception($"User already exist");
        }

        var newUser = _mapper.Map<User>(request);
        newUser.Password = _passwordService.HashPassword(newUser.Password);
        await _usersRepository.Create(newUser);
        await _usersRepository.SaveChangesAsync();
    }
}
