using AutoMapper;
using BookConnect.Application.Common.Models;
using BookConnect.Application.Features.Shelves.Models;
using MediatR;

namespace BookConnect.Application.Features.Shelves.Queries.GetShelvesByUserId;

public class GetShelvesByUserIdQueryHandler : IRequestHandler<GetShelvesByUserIdQuery, PaginatedResults<ShelvesDetailsDto>>
{
    private readonly IShelfRepository _shelfRepository;
    private readonly IMapper _mapper;
    public GetShelvesByUserIdQueryHandler(IShelfRepository shelfRepository, IMapper mapper)
    {
       _shelfRepository = shelfRepository;
       _mapper = mapper; 
    }
    public async Task<PaginatedResults<ShelvesDetailsDto>> Handle(GetShelvesByUserIdQuery request, CancellationToken cancellationToken)
    {
        var (results, pageData) = await _shelfRepository.GetAllValuesPaginated(
            request.Page,
            request.PageSize,
            x => x.OwnerId.ToString() == request.UserId &&
            x.ShelfName.ToLower().Contains(request.ShelfName.ToLower())
        );

        var mappedResults = _mapper.Map<IEnumerable<ShelvesDetailsDto>>(results);

        return new PaginatedResults<ShelvesDetailsDto>(mappedResults, pageData);
    }
}