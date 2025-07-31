using System.ComponentModel.DataAnnotations.Schema;

namespace AI_service.Shared.Domain.Entities
{
    [Table("Vectors")]
    public class Vector
    {
        public Guid Id { get; init; }
        public Guid TextId { get; init; }
        public Guid VectorId { get; init; }
        public DateTime CreatedAt { get; init; }

        public static Vector Create(Guid TextId, Guid vectorId)
        {
            return new()
            {
                Id = Guid.NewGuid(),
                VectorId = vectorId,
                TextId = TextId,
                CreatedAt = DateTime.UtcNow,
            };
        }
    }
}
