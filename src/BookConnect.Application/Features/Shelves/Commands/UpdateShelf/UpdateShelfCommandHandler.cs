using AutoMapper;
using BookConnect.Application.Common.Exceptions;
using BookConnect.Application.Features.Shelves.Models;
using ValidationException = BookConnect.Application.Common.Exceptions.ValidationException;
using FluentValidation;
using MediatR;

namespace BookConnect.Application.Features.Shelves.Commands.UpdateShelf;

sealed class UpdateShelfCommandHandler : IRequestHandler<UpdateShelfCommand>
{
    private readonly IShelfRepository _shelfRepository;
    private readonly IMapper _mapper;
    private readonly IValidator<UpdateShelfDto> _validator;
    public UpdateShelfCommandHandler(IShelfRepository shelfRepository, IMapper mapper, IValidator<UpdateShelfDto> validator)
    {
        _shelfRepository = shelfRepository;
        _mapper = mapper;
        _validator = validator; 
    }
    public async Task Handle(UpdateShelfCommand request, CancellationToken cancellationToken)
    {
        var shelf = await _shelfRepository.GetValue(x => x.Id.ToString() == request.shelfId, AsNoTracking: false)
            ?? throw new NotFoundException($"Shelf with ID '{request.shelfId}' was not found.");


        var shelfToUpdate = _mapper.Map<UpdateShelfDto>(shelf);

        request.UpdateShelf.ApplyTo(shelfToUpdate, (err) => throw new ConflictException("Error in JsonPatchDocument: " + err.ErrorMessage));

        var validationResults = await _validator.ValidateAsync(shelfToUpdate);

        if(!validationResults.IsValid)
            throw new ValidationException(validationResults.Errors);

        var existingShelf = await _shelfRepository.GetValue(x => x.ShelfName.ToLower() == shelfToUpdate.ShelfName.ToLower());

        if(existingShelf != null)
        {
            throw new ConflictException($"Shelf with Name '{shelfToUpdate.ShelfName}' already exists.");
        }

        _mapper.Map(shelfToUpdate, shelf);

        await _shelfRepository.SaveChangesAsync();
    }
}