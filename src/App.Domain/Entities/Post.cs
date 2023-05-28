using App.Domain.Common;

namespace App.Domain.Entities;
public sealed class Post :  BaseEntity
{
    public required string Title { get; set; }
    public required string Body { get; set; }

    public Guid? OwnerId { get; set; }
    public required User Owner { get; set; }

    public Guid? Review_Post_Id { get; set; }
    public ICollection<Likes> Likes { get; set; } = new List<Likes>();
}