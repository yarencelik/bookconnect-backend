namespace BookConnect.Application.Features.Users.Models;

public class CreateUserDto
{
    public required string Username { get; set; }
    public required string Password { get; set; }
    public required string Email { get; set; }
    public string? FirstName { get; set; }
    public string? LastName { get; set; }
}