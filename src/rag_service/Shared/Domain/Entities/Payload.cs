using System.ComponentModel.DataAnnotations.Schema;

namespace AI_service.Shared.Domain.Entities
{
    [Table("Payloads")]
    public class Payload
    {
        public Guid Id { get; init; }
        public string Value { get; init; } = string.Empty;
        public DateTime CreatedAt { get; init; }

        public static Payload Create(string value)
        {
            return new()
            {
                Id = Guid.NewGuid(),
                Value = value,
                CreatedAt = DateTime.UtcNow
            };
        }
    }
}
