using App.Application.Features.Auth;
using App.Application.Features.Author;
using App.Application.Features.Books;
using App.Application.Features.Follow;
using App.Application.Features.Likes;
using App.Application.Features.Posts;
using App.Application.Features.Reviews;
using App.Application.Features.Users;
using App.Infrastructure.Persistence;
using App.Infrastructure.Repositories;
using App.Infrastructure.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace App.Infrastructure;
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

        // Repositories
        services.AddScoped<IUsersRepository, UsersRepository>();
        services.AddScoped<IBooksRepository, BooksRepository>();
        services.AddScoped<IAuthorsRepository, AuthorsRepository>();
        services.AddScoped<IPostsRepository, PostsRepository>();
        services.AddScoped<ILikesRepository, LikesRepository>();
        services.AddScoped<IFollowRepository, FollowRepository>();
        services.AddScoped<IReviewsRepository, ReviewsRepository>();
        return services;
    }
}
