using App.Application.Common.Exceptions;
using App.Application.Features.Author;
using App.Domain.Entities;
using AutoMapper;
using MediatR;

namespace App.Application.Features.Books.Commands.AddBook;

sealed class AddBookCommandHandler : IRequestHandler<AddBookCommand, string>
{
    private readonly IBooksRepository _booksRepository;
    private readonly IAuthorsRepository _authorsRepository;
    private readonly IMapper _mapper;
    public AddBookCommandHandler(IBooksRepository booksRepository, IMapper mapper, IAuthorsRepository authorsRepository)
    {
        _booksRepository = booksRepository;
        _mapper = mapper;
        _authorsRepository = authorsRepository;
    }
    public async Task<string> Handle(AddBookCommand request, CancellationToken cancellationToken)
    {
        // Check if book already exists.
        var book = await _booksRepository.GetValue(x => x.ISBN == request.ISBN);
        if(book != null)
            throw new ConflictException($"Book with Title '{request.Title}' already exists.");
        
        var newBook = _mapper.Map<Book>(request);

        if(!string.IsNullOrWhiteSpace(request.AuthorName))
        {
            var author = await _authorsRepository.GetValue(x => x.AuthorName!.ToLower() == request.AuthorName.ToLower(), null, false)
                ?? throw new NotFoundException($"Author with Name '{request.AuthorName}' was not found.");
            
            newBook.Author = author;
        }
        
        await _booksRepository.Create(newBook);
        await _booksRepository.SaveChangesAsync();

        return newBook.Id.ToString();
    }
}