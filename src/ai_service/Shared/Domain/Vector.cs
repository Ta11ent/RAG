using System.ComponentModel.DataAnnotations.Schema;

namespace AI_service.Shared.Domain
{
    [Table("Vectors")]
    public class Vector
    {
        public Guid Id { get; init; }
        public Guid TextId { get; init; }
        public DateTime CreatedAt { get; init; }

        public static Vector Create(Guid TextId)
        {
            return new()
            {
                Id = Guid.NewGuid(),
                TextId = TextId,
                CreatedAt = DateTime.UtcNow,
            };
        }
    }
}
