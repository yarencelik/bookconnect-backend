namespace App.Application.Features.Auth.Models;
public sealed class AuthDetailsDto
{
    public Guid Id { get; set; }
    public string Username { get; set; } = string.Empty;
    public string AccessToken { get; set; } = string.Empty;
}
