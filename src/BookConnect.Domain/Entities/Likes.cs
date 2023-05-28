using BookConnect.Domain.Common;

namespace BookConnect.Domain.Entities;

public class Likes : BaseEntity
{
    public Guid? Like_User_Id { get; set; }
    public required User Like_User { get; set; }

    public Guid? Like_Post_Id { get; set; }
    public required Post Like_Post { get; set; }
}