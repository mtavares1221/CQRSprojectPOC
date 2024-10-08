using Domain.Abstractions;
using Domain.Enum;
using Infra.Persistence;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using MongoDB.Driver;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace Services.AuthService
{
    public class AuthService(IConfiguration configuration, MongoDbContext context) : IAuthService
    {
        private readonly IConfiguration _configuration = configuration;
        private readonly MongoDbContext _context = context;

        public string GenerateJWT(string email, string username)
        {
            var issuer = _configuration["JWT:Issuer"];
            var audience = _configuration["JWT:Audience"];
            var key = _configuration["JWT:Key"];

            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(key!));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var claims = new List<Claim>
            {
                new("Email", email),
                new("Username", username),
                new("EmailIdentifier", email.Split("@").ToString()!),
                new("CurrentTime", DateTime.Now.ToString())
            };

            _ = int.TryParse(_configuration["JWT:TokenExpirationTimeInDays"], out int tokenExpirationTimeInDays);

            var token = new JwtSecurityToken(issuer: issuer, audience: audience, claims: claims, expires: DateTime.Now.AddDays(tokenExpirationTimeInDays), signingCredentials: credentials);
            var tokenHandler = new JwtSecurityTokenHandler();

            return tokenHandler.WriteToken(token);
        }

        public string GenerateRefreshToken()
        {
            var secureRandomBytes = new byte[128];

            using var randomNumberGenerator = RandomNumberGenerator.Create();

            randomNumberGenerator.GetBytes(secureRandomBytes);

            return Convert.ToBase64String(secureRandomBytes);
        }

        public string HashingPassword(string password)
        {
            byte[] bytes = SHA256.HashData(Encoding.UTF8.GetBytes(password));

            StringBuilder builder = new();

            for(int i = 0; i < bytes.Length; i++)
            {
                builder.Append(bytes[i].ToString("x2"));
            }

            return builder.ToString();
        }

        public async Task<ValidationFieldsUserEnum> UniqueEmailAndUsername(string email, string username)
        {
            var users = await _context.Users.Find(_ => true).ToListAsync();

            var emailExists = users.Any(x => x.Email == email);
            var usernameExists = users.Any(x => x.Username == username);

            if (emailExists && usernameExists)
            {
                return ValidationFieldsUserEnum.UsernameAndEmailUnavailable;
            }
            else if (emailExists)
            {
                return ValidationFieldsUserEnum.EmailUnavailable;
            }
            else if (usernameExists)
            {
                return ValidationFieldsUserEnum.UsernameUnavailable;
            }

            return ValidationFieldsUserEnum.FieldsOk;
        }

    }
}
