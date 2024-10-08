using Domain.Enum;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System.ComponentModel.DataAnnotations;

namespace Domain.Entity
{
    public class ListCard
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; } = ObjectId.GenerateNewId().ToString();

        [Required]
        [StringLength(45, MinimumLength = 2)]
        public string? Title { get; set; }

        public StatusItemEnum Status { get; set; } = StatusItemEnum.Active;

        public DateTime CreatedAt { get; set; } = DateTime.Now;

        public Workspace? Workspace { get; set; }

        public ICollection<Card>? Cards { get; set; }
    }
}
