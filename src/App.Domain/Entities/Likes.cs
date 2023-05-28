using App.Domain.Common;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace App.Domain.Entities;
public class Likes : BaseEntity
{
    public Guid? Like_User_Id { get; set; }
    public required User Like_User { get; set; }

    public Guid? Like_Post_Id { get; set; }
    public required Post Like_Post { get; set; }
}