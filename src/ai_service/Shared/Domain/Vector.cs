using System.ComponentModel.DataAnnotations.Schema;

namespace AI_service.Shared.Domain
{
    internal class Vector
    {
        internal int Id { get; init; }
        internal int TextId { get; init; }
        internal DateTime CreatedAt { get; init; }
        internal int VectorId { get; init; }
    }
}
