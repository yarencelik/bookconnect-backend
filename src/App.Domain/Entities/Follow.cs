using App.Domain.Common;

namespace App.Domain.Entities;

public class Follow : BaseEntity 
{
    public Guid? Follower_Id { get; set; }
    public User? Follower_User { get; set; }

    public Guid? Following_Id { get; set; }
    public User? Following_User { get; set; }
}