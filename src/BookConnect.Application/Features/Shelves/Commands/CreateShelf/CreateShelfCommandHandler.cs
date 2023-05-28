using AutoMapper;
using BookConnect.Application.Common.Exceptions;
using BookConnect.Application.Common.Interfaces;
using BookConnect.Application.Features.Users;
using BookConnect.Domain.Entities;
using MediatR;

namespace BookConnect.Application.Features.Shelves.Commands.CreateShelf;

sealed class CreateShelfCommandHandler : IRequestHandler<CreateShelfCommand, Guid>
{
    private readonly IShelfRepository _shelfRepository;
    private readonly ICurrentUserService _currentUserService;
    private readonly IUsersRepository _usersRepository; 
    private readonly IMapper _mapper;
    public CreateShelfCommandHandler(IShelfRepository shelfRepository, IUsersRepository usersRepository, ICurrentUserService currentUserService, IMapper mapper)
    {
        _shelfRepository = shelfRepository; 
        _usersRepository = usersRepository;
        _currentUserService = currentUserService;
        _mapper = mapper;
    }
    public async Task<Guid> Handle(CreateShelfCommand request, CancellationToken cancellationToken)
    {
        var _ = await _usersRepository.GetValue(x => x.Id.ToString() == _currentUserService.UserId)
            ?? throw new UnauthorizedAccessException("User was not found.");

        var existingShelf = await _shelfRepository.GetValue(x => x.ShelfName.ToLower() == request.ShelfName.ToLower());

        if(existingShelf != null)
        {
            throw new ConflictException($"Shelf with Name '{request.ShelfName}' already exists.");
        }

        var newShelf = _mapper.Map<Shelf>(request);
        newShelf.OwnerId = Guid.Parse(_currentUserService.UserId ?? string.Empty);


        await _shelfRepository.Create(newShelf);
        await _shelfRepository.SaveChangesAsync();

        return newShelf.Id;
    }
}