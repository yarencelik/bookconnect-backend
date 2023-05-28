using App.Application.Common.Models;
using App.Application.Features.Books;
using App.Domain.Entities;
using App.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Infrastructure.Repositories;
public class BooksRepository : RepositoryBase<Book>, IBooksRepository
{
    public BooksRepository(ApplicationDbContext context) : base(context)
    {
    }
}
