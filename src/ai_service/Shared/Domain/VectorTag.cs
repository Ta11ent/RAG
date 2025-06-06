using System.ComponentModel.DataAnnotations.Schema;

namespace AI_service.Shared.Domain
{
    [Table("VectorTags")]
    public class VectorTag
    {
        public Guid VectorId { get; init; }
        public Guid TagId { get; init; }

        public static VectorTag Create(
            Guid vectorId,
            Guid tagId)
        {
            return new VectorTag()
            {
                VectorId = vectorId,
                TagId = tagId
            };
        }
    }
}
