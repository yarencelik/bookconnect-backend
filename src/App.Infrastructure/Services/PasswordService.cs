using System.Text;
using App.Application.Features.Auth;
using Isopoh.Cryptography.Argon2;
using Microsoft.Extensions.Configuration;

namespace App.Infrastructure.Services;

public sealed class PasswordService : IPasswordService
{
    private readonly IConfiguration _config;
    private string Salt { get; set; }
    public PasswordService(IConfiguration config)
    {
        _config = config;   
        Salt = _config["PasswordSettings:Salt"] ?? throw new NullReferenceException("Salt should be provided.");
    }
    public string HashPassword(string password)
    {
        var config = new Argon2Config()
        {
            Salt = Encoding.UTF8.GetBytes(Salt),
            Password = Encoding.UTF8.GetBytes(password)
        };

        return Argon2.Hash(config);
    }

    public bool VerifyPassword(string hashPassword, string password)
    {
        var config = new Argon2Config
        {
            Salt = Encoding.UTF8.GetBytes(Salt),
            Password = Encoding.UTF8.GetBytes(password),   
        };

        var result = Argon2.Verify(hashPassword, config);
        
        return result;
    }
}