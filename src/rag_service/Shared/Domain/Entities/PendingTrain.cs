using System.ComponentModel.DataAnnotations.Schema;

namespace AI_service.Shared.Domain.Entities
{
    [Table("PandingTrains")]
    public class PendingTrain
    {
        public Guid Id { get; init; }
        public Guid PayloadId { get; init; }
        public DateTime CreatedAt { get; init; }

        public static PendingTrain Create(Guid payloadId)
        {
            return new PendingTrain
            {
                Id = Guid.NewGuid(),
                PayloadId = payloadId,
                CreatedAt = DateTime.UtcNow
            };
        }
    }
}
