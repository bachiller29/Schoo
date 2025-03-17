namespace FinanzautoShool.Aplication.Services.Auth.Interface;

public interface IAuthService
{
    string GenerateToken(string username);
}