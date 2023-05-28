using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Application.Features.Posts.Models;
public sealed class CreatePostDto
{
    public required string Title { get; set; }
    public required string Body { get; set; }
}
