using BookConnect.Domain.Common;
using BookConnect.Domain.Enums;

namespace BookConnect.Domain.Entities;
public sealed class User : BaseEntity
{
    public required string Username { get; set; }
    public required string Password { get; set; }
    public required string Email { get; set; }
    public string? FirstName { get; set; }
    public string? LastName { get; set; }

    public UserRole Role { get; set; }

    public Author? Author { get; set; }

    public ICollection<Follow> Followers { get; set; } = new List<Follow>();
    public ICollection<Follow> Followings { get; set; } = new List<Follow>();
    public ICollection<Post> Posts { get; set; } = new List<Post>();

    public ICollection<Likes> LikedPost { get; set; } = new List<Likes>();
    public ICollection<Review> BookReviews { get; set; } = new List<Review>();

    public ICollection<Shelf> Shelves { get; set; } = new List<Shelf>();
}