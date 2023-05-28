using App.Application.Common.Models;
using App.Application.Features.Author.Models;

namespace App.Application.Features.Author.Queries.GetAuthors;

public class GetAuthorsQuery : BaseQuery<AuthorDetailsDto>
{
    public string Name { get; set; }  = string.Empty;
}