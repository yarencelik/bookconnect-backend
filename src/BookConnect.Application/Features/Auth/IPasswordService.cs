namespace App.Application.Features.Auth;

public interface IPasswordService
{
   string HashPassword(string password);

   bool VerifyPassword(string encodedPassword, string password);
}