using BookConnect.Application.Features.Books.Models;
using BookConnect.Application.Features.Users.Models;

namespace BookConnect.Application.Features.Reviews.Models;

public class ReviewDetailsDto
{
    public Guid Id { get; set; }
    public int Rating { get; set; }
    public string? Description { get; set; }

    public Guid Reviewer_Id { get; set; }
    public UserDetailsDto? Reviewer { get; set; }

    public Guid Book_Id { get; set; }
    public BookDetailsDto? Book { get; set; }
}