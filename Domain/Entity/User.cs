using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Entity
{
    public class User
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; } = ObjectId.GenerateNewId().ToString();

        [BsonRequired]
        [BsonElement("name")]
        public string? Name { get; set; }

        [BsonElement("surname")]
        public string? Surname { get; set; }

        [BsonRequired]
        [BsonElement("email")]
        public string? Email { get; set; }

        [BsonRequired]
        [BsonElement("passwordHash")]
        public string? PasswordHash { get; set; }

        [BsonRequired]
        [BsonElement("username")]
        public string? Username { get; set; }

        [BsonElement("workspaces")]
        public List<string>? WorkspaceIds { get; set; } // IDs das Workspaces

        [BsonElement("refreshToken")]
        public string? RefreshToken { get; set; }

        [BsonElement("refreshTokenExpirationTime")]
        public DateTime? RefreshTokenExpirationTime { get; set; }
    }
}
