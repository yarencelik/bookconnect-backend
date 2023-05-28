using BookConnect.Application.Common.Models;
using BookConnect.Application.Features.Author.Models;

namespace BookConnect.Application.Features.Author.Queries.GetAuthors;
public class GetAuthorsQuery : BaseQuery<AuthorDetailsDto>
{
    public string Name { get; set; }  = string.Empty;
}