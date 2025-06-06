using System.ComponentModel.DataAnnotations.Schema;

namespace AI_service.Shared.Domain
{
    [Table("Vectors")]
    public class Vector
    {
        public Guid Id { get; init; }
        public Guid TextId { get; init; }
        public DateTime CreatedAt { get; init; }
        public Guid VectorId { get; init; }

        public static Vector Create(
            Guid TextId, 
            DateTime CreatedAt, 
            Guid VecoreId)
        {
            return new()
            {
                TextId = TextId,
                CreatedAt = CreatedAt,
                VectorId = VecoreId
            };
        }
    }
}
