using System.ComponentModel.DataAnnotations.Schema;

namespace AI_service.Shared.Domain.Entities
{
    [Table("Vectors")]
    public class Vector
    {
        public Guid Id { get; init; }
        public Guid PayloadId { get; init; }
        public Guid VectorId { get; init; }
        public DateTime CreatedAt { get; init; }

        public static Vector Create(Guid payloadId, Guid vectorId)
        {
            return new()
            {
                Id = Guid.NewGuid(),
                VectorId = vectorId,
                PayloadId = payloadId,
                CreatedAt = DateTime.UtcNow,
            };
        }
    }
}
