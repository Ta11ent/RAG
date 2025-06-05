using System.ComponentModel.DataAnnotations.Schema;

namespace AI_service.Shared.Domain
{
    [Table("VectorTags")]
    internal class VectorTag
    {
        internal int VectorId { get; init; }
        internal int TagId { get; init; }
    }
}
