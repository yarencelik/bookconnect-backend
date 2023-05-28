using System.Linq.Expressions;
using AutoMapper;
using BookConnect.Application.Features.Auth;
using BookConnect.Domain.Entities;
using MediatR;

namespace BookConnect.Application.Features.Users.Commands.CreateUser;

// TODO: Improvement:  Register as Author Directly
// Which can be "Verified" by admin if the Author is legitimate
// and also verify if the Author has a linked account.
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
