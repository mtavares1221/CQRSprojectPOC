using Domain.Enum;

namespace Domain.Abstractions
{
    public interface IAuthService
    {
        string GenerateJWT(string email, string username);
        string GenerateRefreshToken();
        string HashingPassword(string password);

        // Marque o método como assíncrono
        Task<ValidationFieldsUserEnum> UniqueEmailAndUsername(string email, string username);
    }

}
