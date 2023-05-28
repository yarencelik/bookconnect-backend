using MediatR;

namespace App.Application.Common.Models;
public abstract class BaseQuery<T> : IRequest<PaginatedResults<T>> where T : class
{
    public int Page { get; set; } = 1;
    public int PageSize { get; set; } = 10;
}
