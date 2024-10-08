using Domain.Enum;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System.ComponentModel.DataAnnotations;

namespace Domain.Entity
{
    public class Card
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; } = ObjectId.GenerateNewId().ToString();

        [Required]
        [StringLength(45)]
        public string? Title { get; set; }

        [StringLength(120)]
        public string? Description { get; set; }

        public DateTime? CreatedAt { get; set; } = DateTime.Now;

        public DateTime? Deadline { get; set; }

        public ListCard? List { get; set; }

        public StatusCardEnum Status { get; set; }
    }
}
