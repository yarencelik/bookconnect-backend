using BookConnect.Application.Features.Auth;
using BookConnect.Application.Features.Author;
using BookConnect.Application.Features.Books;
using BookConnect.Application.Features.Follow;
using BookConnect.Application.Features.Likes;
using BookConnect.Application.Features.Posts;
using BookConnect.Application.Features.Reviews;
using BookConnect.Application.Features.Shelves;
using BookConnect.Application.Features.Users;
using BookConnect.Infrastructure.Persistence;
using BookConnect.Infrastructure.Repositories;
using BookConnect.Infrastructure.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace BookConnect.Infrastructure;

public static class ConfigureServices
{
    public static IServiceCollection AddInfrastructureServices(this IServiceCollection services, IConfiguration config)
    {
        services.AddDbContext<ApplicationDbContext>();
        services.AddStackExchangeRedisCache(cfg =>
        {
            cfg.Configuration = config["CacheSettings:ConnectionString"];
        });

        // Services
        services.AddScoped<IJwtService, JwtService>();
        services.AddScoped<IPasswordService, PasswordService>();
        services.AddScoped<IShelfService, ShelfService>();

        // Repositories
        services.AddScoped<IUsersRepository, UsersRepository>();
        services.AddScoped<IBooksRepository, BooksRepository>();
        services.AddScoped<IAuthorsRepository, AuthorsRepository>();
        services.AddScoped<IPostsRepository, PostsRepository>();
        services.AddScoped<ILikesRepository, LikesRepository>();
        services.AddScoped<IFollowRepository, FollowRepository>();
        services.AddScoped<IReviewsRepository, ReviewsRepository>();
        services.AddScoped<IShelfRepository, ShelfRepository>();
        services.AddScoped<IBookShelfRepository, BookShelfRepository>();

        return services;
    }
}
