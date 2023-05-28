﻿using App.Application.Features.Auth;
using App.Domain.Common;
using App.Domain.Entities;
using App.Infrastructure.Persistence.Seeds;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System.Reflection;


namespace App.Infrastructure.Persistence;
public sealed class ApplicationDbContext : DbContext
{
    public DbSet<User> Users => Set<User>();
    public DbSet<Author> Authors => Set<Author>();
    public DbSet<Book> Books => Set<Book>();
    public DbSet<Post> Posts => Set<Post>();
    public DbSet<Likes> Likes => Set<Likes>();
    public DbSet<Follow> Follows => Set<Follow>();
    public DbSet<Review> Reviews => Set<Review>();

    private readonly IConfiguration _config;
    private readonly IPasswordService _passwordService;

    public ApplicationDbContext(IConfiguration config, IPasswordService passwordService)
    {
        _config = config;
        _passwordService = passwordService;
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseNpgsql(_config["ConnectionStrings:DB"]);
        base.OnConfiguring(optionsBuilder);
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        modelBuilder.AddInitialData(_config, _passwordService);

        base.OnModelCreating(modelBuilder);
    }

    public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        foreach (var item in ChangeTracker.Entries<BaseEntity>())
        {
            if(item.State == EntityState.Added)
            {
                item.Entity.CreatedAt = DateTime.UtcNow;
                item.Entity.UpdatedAt = DateTime.UtcNow;
                break;
            }

            if(item.State == EntityState.Modified)
            {
                item.Entity.UpdatedAt = DateTime.UtcNow;
                break;
            }
        }
        return base.SaveChangesAsync(cancellationToken);
    }
}