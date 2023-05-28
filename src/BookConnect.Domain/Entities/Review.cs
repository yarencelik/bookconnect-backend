using App.Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Domain.Entities;
public sealed class Review : BaseEntity
{
    public int Rating { get; set; }
    public string? Description { get; set; }

    public Guid Reviewer_Id { get; set; }
    public User? Reviewer { get; set; }

    public Guid Book_Id { get; set; }
    public Book? Book { get; set; }
}
